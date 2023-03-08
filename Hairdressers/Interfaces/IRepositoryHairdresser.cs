using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositoryHairdresser {

        Hairdresser? FindHairdresser(int hairdresser_id);
        List<Hairdresser> GetHairdressers();
        List<Hairdresser> GetHairdressers(int user_id);
        Task InsertHairdresserAsync(string name, string phone, string address, int postal_code, int user_id);
        Task UpdateHairdresserAsync(int hairdresser_id, string name, string phone, string address, int postal_code, int user_id);
        Task DeleteHairdresserAsync(int hairdresser_id);

    }
}
