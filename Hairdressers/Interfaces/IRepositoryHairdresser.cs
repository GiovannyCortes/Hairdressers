using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositoryHairdresser {

        List<Hairdresser> GetHairdressers(int user_id);

    }
}
