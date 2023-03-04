using Hairdressers.Data;
using Hairdressers.Interfaces;

namespace Hairdressers.Repositories {
    public class RepositoryUsers : IRepositoryUser {

        private HairdressersContext context;

        public RepositoryUsers(HairdressersContext context) { 
            this.context = context;
        }

        public string ValidateUser(string email, string password) {
            var consulta = from datos in context.Users
                           where datos.Password == password &&
                                 datos.Email == email
                           select datos.Email;
            string result = consulta.ToList().First();
            return (result == null)? "" : result;
        }

    }
}
