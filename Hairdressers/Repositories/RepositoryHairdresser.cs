using Hairdressers.Data;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using System.Xml.Linq;

namespace Hairdressers.Repositories {
    public class RepositoryHairdresser : IRepositoryHairdresser {

        HairdressersContext context;

        public RepositoryHairdresser(HairdressersContext context) {
            this.context = context;
        }

        public Hairdresser? FindHairdresser(int hairdresser_id) {
            var consulta = from data in this.context.Hairdressers
                           where data.HairdresserId == hairdresser_id
                           select new Hairdresser {
                               HairdresserId = data.HairdresserId,
                               Name = data.Name,
                               Address = data.Address,
                               PostalCode = data.PostalCode,
                               Phone = data.Phone ?? "Sin número de teléfono"
                           };
            return consulta.ToList().FirstOrDefault();
        }

        public List<Hairdresser> GetHairdressers() {
            var consulta = from data in this.context.Hairdressers
                           select new Hairdresser {
                               HairdresserId = data.HairdresserId,
                               Name = data.Name,
                               Address = data.Address,
                               PostalCode = data.PostalCode,
                               Phone = data.Phone ?? "Sin número de teléfono"
                           };
            return consulta.ToList();
        }

        public List<Hairdresser> GetHairdressers(int user_id) {
            var consulta = context.Hairdressers
                           .Join(
                               context.Admins,
                               hairdresser => hairdresser.HairdresserId,
                               admin => admin.HairdresserId,
                               (hairdresser, admin) => new { Hairdresser = hairdresser, Admin = admin }
                           )
                           .Join(
                               context.Users,
                               admin => admin.Admin.UserId,
                               user => user.UserId,
                               (admin, user) => new { Hairdresser = admin.Hairdresser, User = user }
                           )
                           .Where(x => x.User.UserId == user_id)
                           .Select(x => new Hairdresser {
                                        HairdresserId = x.Hairdresser.HairdresserId,
                                        Name = x.Hairdresser.Name,
                                        Address = x.Hairdresser.Address,
                                        PostalCode = x.Hairdresser.PostalCode,
                                        Phone = x.Hairdresser.Phone ?? "Sin número de teléfono"
                                  });
            return consulta.ToList();
        }

        public async Task<int> InsertHairdresserAsync(string name, string phone, string address, int postal_code, int user_id) {
            var newid = this.context.Hairdressers.Any() ? this.context.Hairdressers.Max(s => s.HairdresserId) + 1 : 1;
            Hairdresser hairdresser = new Hairdresser {
                HairdresserId = newid,
                Name = name,
                Phone = phone,
                Address = address,
                PostalCode = postal_code
            };
            this.context.Hairdressers.Add(hairdresser);
            // falta meter admin
            await this.context.SaveChangesAsync();
            return newid;
        }

        public async Task UpdateHairdresserAsync(int hairdresser_id, string name, string phone, string address, int postal_code) {
            Hairdresser? hairdresser = this.FindHairdresser(hairdresser_id);
            if (hairdresser != null) {
                hairdresser.Name = name;
                hairdresser.Phone = phone;
                hairdresser.Address = address;
                hairdresser.PostalCode = postal_code;
                await this.context.SaveChangesAsync();
            }
        }

        public async Task DeleteHairdresserAsync(int hairdresser_id) {
            Hairdresser? hairdresser = this.FindHairdresser(hairdresser_id);
            if (hairdresser != null) {
                this.context.Hairdressers.Remove(hairdresser);
                await this.context.SaveChangesAsync();
            }
        }

    }
}
