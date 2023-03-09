using Hairdressers.Data;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using System.Data;

namespace Hairdressers.Repositories {

    public enum AdminRole { Propietario = 1, Gerente = 2, Supervisor = 3, Empleado = 4 }
    public enum ServerRes { OK = 0, RegistroExistente = 1, RegistroNoEncontrado = 2 }

    public class RepositoryAdmin : IRepositoryAdmin {

        private HairdressersContext context;

        public RepositoryAdmin(HairdressersContext context) {
            this.context = context;
        }

        public Admin? FindAdmin(int hairdresser_id, int user_id) {
            var consulta = from datos in this.context.Admins
                           where datos.HairdresserId == hairdresser_id && datos.UserId == user_id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public List<Admin> GetAdmins(int hairdresser_id) {
            var consulta = from datos in this.context.Admins
                           where datos.HairdresserId == hairdresser_id
                           select datos;
            return consulta.ToList();
        }

        public async Task<int> InsertAdminAsync(int hairdresser_id, int user_id, AdminRole role) {
            Admin? admin = this.FindAdmin(hairdresser_id, user_id);
            if (admin == null) {
                Admin new_admin = new Admin {
                    HairdresserId = hairdresser_id,
                    UserId = user_id,
                    Role = (int)role
                };
                this.context.Admins.Add(new_admin);
                await this.context.SaveChangesAsync();
                return (int)ServerRes.OK;
            } else { return (int)ServerRes.RegistroExistente; }
        }

        public async Task<int> UpdateAdminAsync(int hairdresser_id, int user_id, AdminRole role) {
            Admin? admin = this.FindAdmin(hairdresser_id, user_id);
            if (admin != null) {
                admin.Role = (int)role;
                await this.context.SaveChangesAsync();
                return (int)ServerRes.OK;
            } else { return (int)ServerRes.RegistroNoEncontrado; }
        }

        public async Task DeleteAdminAsync(int hairdresser_id, int user_id) {
            Admin? admin = this.FindAdmin(hairdresser_id, user_id);
            if (admin != null) {
                this.context.Admins.Remove(admin);
                await this.context.SaveChangesAsync();
            }
        }

    }
}
