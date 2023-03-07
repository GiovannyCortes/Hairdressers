using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositorySchedule {

        List<string> GetNameSchedules(int hairdresser_id);
        List<Schedule> GetSchedules(int hairdresser_id);
        Schedule FindSchedule(int schedule_id);
        Task InsertScheduleAsync(int hairdresser_id, string name, bool active);
        Task UpdateScheduleAsync(int schedule_id, int hairdresser_id, string name, bool active);
        Task DeleteScheduleAsync(int schedule_id);

    }
}
