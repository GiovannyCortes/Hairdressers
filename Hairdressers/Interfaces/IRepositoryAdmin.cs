using Hairdressers.Models;
using Hairdressers.Repositories;

namespace Hairdressers.Interfaces {
    public interface IRepositoryAdmin {

        Admin? FindAdmin(int hairdresser_id, int user_id);
        List<Admin> GetAdmins(int hairdresser_id);
        Task<int> InsertAdminAsync(int hairdresser_id, int user_id, AdminRole role);
        Task<int> UpdateAdminAsync(int hairdresser_id, int user_id, AdminRole role);
        Task DeleteAdminAsync(int hairdresser_id, int user_id);

    }
}
