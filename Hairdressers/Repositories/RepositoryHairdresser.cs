﻿using Hairdressers.Data;
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
            var consulta = from data in context.Hairdressers
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

    }
}
