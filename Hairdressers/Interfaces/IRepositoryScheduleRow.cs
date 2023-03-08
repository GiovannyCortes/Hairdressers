using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositoryScheduleRow {

        List<Schedule_Row> GetScheduleRows(int schedule_id);
        Schedule_Row? FindScheduleRow(int schedule_row_id);
        Task<int> InsertScheduleRowsAsync(int schid, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun);
        Task<int> UpdateScheduleRowsAsync(int schedule_row_id, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun);
        Task DeleteScheduleRowsAsync(int schedule_row_id);

    }
}
