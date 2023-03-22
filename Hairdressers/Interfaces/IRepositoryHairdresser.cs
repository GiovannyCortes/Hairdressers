using Hairdressers.Repositories;
using Hairdressers.Models;

namespace Hairdressers.Interfaces {
    public interface IRepositoryHairdresser {

        #region ADMIN
        Task<bool> AdminExistAsync(int hairdresser_id, int user_id);
        Task<Admin?> FindAdminAsync(int hairdresser_id, int user_id);
        Task<List<Admin>> GetAdminsAsync(int hairdresser_id);
        Task<int> InsertAdminAsync(int hairdresser_id, int user_id, AdminRole role);
        Task<int> UpdateAdminAsync(int hairdresser_id, int user_id, AdminRole role);
        Task<int> DeleteAdminAsync(int hairdresser_id, int user_id_affect, int user_id_action);
        #endregion

        #region USER
        bool IsAdmin(int user_id);
        Task<User?> FindUserAsync(int user_id);
        Task<User?> ValidateUserAsync(string email, string password);
        Task<User> InsertUserAsync(string password, string name, string lastname, string phone, string email, bool econfirmed);
        #endregion

        #region HAIRDRESSER
        Task<Hairdresser?> FindHairdresserAsync(int hairdresser_id);
        Task<List<Hairdresser>> GetHairdressersAsync();
        Task<List<Hairdresser>> GetHairdressersAsync(int user_id);
        Task<List<Hairdresser>> GetHairdressersByFilter(string filter);
        Task<int> InsertHairdresserAsync(string name, string phone, string address, int postal_code, int user_id);
        Task UpdateHairdresserAsync(int hairdresser_id, string name, string phone, string address, int postal_code);
        Task DeleteHairdresserAsync(int hairdresser_id);
        #endregion

        #region SCHEDULE
        Task<List<string>> GetNameSchedulesAsync(int hairdresser_id);
        Task<List<Schedule>> GetSchedulesAsync(int hairdresser_id, bool getrows);
        Task<Schedule?> FindScheduleAsync(int schedule_id);
        Task<Schedule?> FindActiveScheduleAsync(int hairdresser_id);
        Task<int> InsertScheduleAsync(int hairdresser_id, string name, bool active);
        Task UpdateScheduleAsync(int schedule_id, int hairdresser_id, string name, bool active);
        Task DeleteScheduleAsync(int schedule_id);
        #endregion

        #region SCHEDULE_ROW
        Task<List<Schedule_Row>> GetScheduleRowsAsync(int schedule_id);
        Task<List<Schedule_Row>> GetActiveScheduleRowsAsync(int hairdresser_id);
        Task<Schedule_Row?> FindScheduleRowAsync(int schedule_row_id);
        Task<int> InsertScheduleRowsAsync(int schid, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun);
        Task<int> UpdateScheduleRowsAsync(int schedule_row_id, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun);
        Task DeleteScheduleRowsAsync(int schedule_row_id);
        #endregion

        #region APPOINTMENTS
        Task<Appointment?> FindAppoinmentAsync(int appointment_id);
        Task<List<Appointment>> GetAppointmentsByHairdresserAsync(int hairdresser_id);
        Task<List<Appointment>> GetAppointmentsByUserAsync(int user_id);
        Task<int> InsertAppointmentAsync(int user_id, int hairdresser_id, DateTime date, TimeSpan time);
        Task UpdateAppointmentAsync(int appointment_id, DateTime date, TimeSpan time);
        Task DeleteAppointmentAsync(int appointment_id);
        #endregion

        #region SERVICES
        Task<Service?> FindServiceAsync(int service_id);
        Task<List<Service>> GetServicesByHairdresserAsync(int hairdresser_id);
        Task<List<Service>> GetServicesByAppointmentAsync(int appoinment_id);
        Task<List<Service>> GetServicesByIdentificationAsync(List<int> app_services);
        Task InsertServiceAsync(int hairdresser_id, string name, decimal price, byte duracion);
        Task UpdateServiceAsync(int service_id, string name, decimal price, byte duracion);
        Task DeleteServiceAsync(int service_id);
        #endregion

        #region APPOINTMENT_SERVICES
        Task<Appointment_Service?> FindAppointmentServiceAsync(int appointment_id, int service_id);
        Task<List<int>> GetAppointmentServiceAsync(int appointment_id);
        Task InsertAppointmentServiceAsync(int appointment_id, int service_id);
        Task DeleteAppointmentServiceAsync(int appointment_id, int service_id);
        #endregion

    }
}
