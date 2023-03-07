using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositoryHairdresser {

        List<Hairdresser> GetHairdressers(int user_id);
        Hairdresser? FindHairdresser(int hairdresser_id);

    }
}
