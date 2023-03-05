using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositoryUser {

        User? ValidateUser(string email, string password);
        bool IsAdmin(int user_id);
        Task<User> InsertUserAsync(string password, string name, string lastname, string phone, string email, bool econfirmed);
    }
}
