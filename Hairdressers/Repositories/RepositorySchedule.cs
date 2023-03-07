using Hairdressers.Interfaces;
using Hairdressers.Models;

namespace Hairdressers.Repositories {
    public class RepositorySchedule : IRepositorySchedule {

        public Task DeleteScheduleAsync(int schedule_id) {
            throw new NotImplementedException();
        }

        public Schedule FindSchedule(int schedule_id) {
            throw new NotImplementedException();
        }

        public List<string> GetNameSchedules(int hairdresser_id) {
            throw new NotImplementedException();
        }

        public List<Schedule> GetSchedules(int hairdresser_id) {
            throw new NotImplementedException();
        }

        public Task InsertScheduleAsync(int hairdresser_id, string name, bool active) {
            throw new NotImplementedException();
        }

        public Task UpdateScheduleAsync(int schedule_id, int hairdresser_id, string name, bool active) {
            throw new NotImplementedException();
        }

    }
}