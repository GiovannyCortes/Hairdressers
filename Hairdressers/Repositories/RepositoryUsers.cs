using Hairdressers.Data;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

#region VIEWS

#endregion

namespace Hairdressers.Repositories {
    public class RepositoryUsers : IRepositoryUser {

        private HairdressersContext context;

        public RepositoryUsers(HairdressersContext context) { 
            this.context = context;
        }

        public User? ValidateUser(string email, string password) {
            var consulta = from datos in context.Users
                           where datos.Password == password &&
                                 datos.Email == email
                           select datos;
            User? user = consulta.ToList().FirstOrDefault();
            return user;
        }

        public async Task<User> InsertUserAsync
            (string password, string name, string lastname, string phone, string email, bool econfirmed) {

            var newid = context.Users.Any() ? context.Users.Max(u => u.UserId) + 1 : 1;

            User user = new User {
                UserId = newid,
                Password = password,
                Name = name,
                LastName = lastname,
                Phone = phone,
                Email = email,
                EmailConfirmed = econfirmed
            };

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            return user;
        }

        public bool IsAdmin(int user_id) {
            return context.Admins.Any(a => a.UserId == user_id);
        }
    }
}