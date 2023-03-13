using Hairdressers.Repositories;
using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositoryHairdresser {

        #region ADMIN
        Admin? FindAdmin(int hairdresser_id, int user_id);
        List<Admin> GetAdmins(int hairdresser_id);
        bool CompareAdminRole(int hairdresser_id, int user_id_action, int user_id_affect); // True: user_id1 > user_id2
        Task<int> InsertAdminAsync(int hairdresser_id, int user_id, AdminRole role);
        Task<int> UpdateAdminAsync(int hairdresser_id, int user_id, AdminRole role);
        Task<int> DeleteAdminAsync(int hairdresser_id, int user_id_affect, int user_id_action);
        #endregion

        #region USER
        User? ValidateUser(string email, string password);
        bool IsAdmin(int user_id);
        Task<User> InsertUserAsync(string password, string name, string lastname, string phone, string email, bool econfirmed);
        #endregion

        #region HAIRDRESSER
        Hairdresser? FindHairdresser(int hairdresser_id);
        List<Hairdresser> GetHairdressers();
        List<Hairdresser> GetHairdressers(int user_id);
        Task<int> InsertHairdresserAsync(string name, string phone, string address, int postal_code, int user_id);
        Task UpdateHairdresserAsync(int hairdresser_id, string name, string phone, string address, int postal_code);
        Task DeleteHairdresserAsync(int hairdresser_id);
        #endregion

        #region SCHEDULE
        List<string> GetNameSchedules(int hairdresser_id);
        List<Schedule> GetSchedules(int hairdresser_id, bool getrows);
        Schedule? FindSchedule(int schedule_id);
        Task<int> InsertScheduleAsync(int hairdresser_id, string name, bool active);
        Task UpdateScheduleAsync(int schedule_id, int hairdresser_id, string name, bool active);
        Task DeleteScheduleAsync(int schedule_id);
        #endregion

        #region SCHEDULE_ROW
        List<Schedule_Row> GetScheduleRows(int schedule_id);
        Schedule_Row? FindScheduleRow(int schedule_row_id);
        Task<int> InsertScheduleRowsAsync(int schid, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun);
        Task<int> UpdateScheduleRowsAsync(int schedule_row_id, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun);
        Task DeleteScheduleRowsAsync(int schedule_row_id);
        #endregion

    }
}
