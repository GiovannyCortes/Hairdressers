using Hairdressers.Data;
using Hairdressers.Helpers;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;

#region PROCEDURES_ADMIN
/*
    CREATE PROCEDURE SP_COMPARE_ROLE(@HAIRDRESSER_ID INT, @USER_ID1 INT, @USER_ID2 INT, @RES BIT OUT)
    AS
	    DECLARE @ROLE_1 INT, @ROLE_2 INT;
	    SELECT @ROLE_1 = ADMINS.role FROM ADMINS WHERE ADMINS.[user_id] = @USER_ID1 AND ADMINS.hairdresser_id = @HAIRDRESSER_ID;
	    SELECT @ROLE_2 = ADMINS.role FROM ADMINS WHERE ADMINS.[user_id] = @USER_ID2 AND ADMINS.hairdresser_id = @HAIRDRESSER_ID;
	    IF(@ROLE_1 <= @ROLE_2)
		    SET @RES = 1;
	    ELSE
		    SET @RES = 0;
    GO
 */
#endregion

namespace Hairdressers.Repositories {

    public enum AdminRole { Propietario = 1, Gerente = 2, Supervisor = 3, Empleado = 4 }
    public enum ServerRes { OK = 0, ExistingRecord = 1, RecordNotFound = 2, DeleteWithOneAdmin = 3, NotAuthorized = 4 }
    public enum Validates { No_Encontrado = -1, Ok = 0, Rango_Sobreescrito = 1, Duplicado = 2, Rango_incorrecto = 3 }

    public class RepositoryHairdresser : IRepositoryHairdresser {

        HairdressersContext context;

        public RepositoryHairdresser(HairdressersContext context) {
            this.context = context;
        }

        #region USER
        public User? ValidateUser(string email, string password) {
            User? user = this.context.Users.FirstOrDefault(z => z.Email == email);
            if (user == null) {
                return null;
            } else {
                byte[] passUsuario = user.Password;
                byte[] temp = HelperCryptography.EncryptContent(password, user.Salt);

                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temp);
                return (respuesta) ? user : null;
            }
        }

        public async Task<User> InsertUserAsync
            (string password, string name, string lastname, string phone, string email, bool econfirmed) {

            var newid = this.context.Users.Any() ? this.context.Users.Max(u => u.UserId) + 1 : 1;
            string salt = HelperCryptography.GenerateSalt();

            User user = new User {
                UserId = newid,
                Salt = salt,
                Password = HelperCryptography.EncryptContent(password, salt),
                PasswordRead = password,
                Name = name,
                LastName = lastname,
                Phone = phone,
                Email = email,
                EmailConfirmed = econfirmed
            };

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            return user;
        }

        public bool IsAdmin(int user_id) {
            return this.context.Admins.Any(a => a.UserId == user_id);
        }
        #endregion

        #region ADMIN
        public Admin? FindAdmin(int hairdresser_id, int user_id) {
            var consulta = from datos in this.context.Admins
                           where datos.HairdresserId == hairdresser_id && datos.UserId == user_id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public List<Admin> GetAdmins(int hairdresser_id) {
            var consulta = from datos in this.context.Admins
                           where datos.HairdresserId == hairdresser_id
                           select datos;
            return consulta.ToList();
        }

        public bool CompareAdminRole(int hairdresser_id, int user_id_action, int user_id_affect) {
            string sql = "SP_COMPARE_ROLE @HAIRDRESSER_ID, @USER_ID1, @USER_ID2, @RES OUT";
            
            SqlParameter pam_hid = new SqlParameter("@HAIRDRESSER_ID", hairdresser_id);
            SqlParameter pam_us1 = new SqlParameter("@USER_ID1", user_id_action);
            SqlParameter pam_us2 = new SqlParameter("@USER_ID2", user_id_affect);
            SqlParameter pam_res = new SqlParameter("@RES", SqlDbType.Bit);
                         pam_res.Direction = ParameterDirection.Output;

            this.context.Database.ExecuteSqlRaw(sql, pam_hid, pam_us1, pam_us2, pam_res);
            return (bool)pam_res.Value;
        }

        public async Task<int> InsertAdminAsync(int hairdresser_id, int user_id, AdminRole role) {
            Admin? admin = this.FindAdmin(hairdresser_id, user_id);
            if (admin == null) {
                Admin new_admin = new Admin {
                    HairdresserId = hairdresser_id,
                    UserId = user_id,
                    Role = (int)role
                };
                this.context.Admins.Add(new_admin);
                await this.context.SaveChangesAsync();
                return (int)ServerRes.OK;
            } else { return (int)ServerRes.ExistingRecord; }
        }

        public async Task<int> UpdateAdminAsync(int hairdresser_id, int user_id, AdminRole role) {
            Admin? admin = this.FindAdmin(hairdresser_id, user_id);
            if (admin != null) {
                admin.Role = (int)role;
                await this.context.SaveChangesAsync();
                return (int)ServerRes.OK;
            } else { return (int)ServerRes.RecordNotFound; }
        }

        public async Task<int> DeleteAdminAsync(int hairdresser_id, int user_id_affect, int user_id_action) {
            if (this.CompareAdminRole(hairdresser_id, user_id_action, user_id_affect)) { // Devolverá TRUE si el rango es >= que el afectado
                if (this.GetAdmins(hairdresser_id).Count == 1) {
                    return (int)ServerRes.DeleteWithOneAdmin;
                } else {
                    Admin? admin = this.FindAdmin(hairdresser_id, user_id_affect);
                    if (admin != null) {
                        this.context.Admins.Remove(admin);
                        await this.context.SaveChangesAsync();
                    }
                    return (int)ServerRes.OK;
                }
            } else { return (int)ServerRes.NotAuthorized; }
        }
        #endregion

        #region HAIRDRESSER
        public Hairdresser? FindHairdresser(int hairdresser_id) {
            var consulta = from data in this.context.Hairdressers
                           where data.HairdresserId == hairdresser_id
                           select new Hairdresser {
                               HairdresserId = data.HairdresserId,
                               Name = data.Name,
                               Address = data.Address,
                               PostalCode = data.PostalCode,
                               Phone = data.Phone ?? "Sin número de teléfono"
                           };
            return consulta.ToList().FirstOrDefault();
        }

        public List<Hairdresser> GetHairdressers() {
            var consulta = from data in this.context.Hairdressers
                           select new Hairdresser {
                               HairdresserId = data.HairdresserId,
                               Name = data.Name,
                               Address = data.Address,
                               PostalCode = data.PostalCode,
                               Phone = data.Phone ?? "Sin número de teléfono"
                           };
            return consulta.ToList();
        }

        public List<Hairdresser> GetHairdressers(int user_id) {
            var consulta = context.Hairdressers
                           .Join(
                               context.Admins,
                               hairdresser => hairdresser.HairdresserId,
                               admin => admin.HairdresserId,
                               (hairdresser, admin) => new { Hairdresser = hairdresser, Admin = admin }
                           )
                           .Join(
                               context.Users,
                               admin => admin.Admin.UserId,
                               user => user.UserId,
                               (admin, user) => new { Hairdresser = admin.Hairdresser, User = user }
                           )
                           .Where(x => x.User.UserId == user_id)
                           .Select(x => new Hairdresser {
                                        HairdresserId = x.Hairdresser.HairdresserId,
                                        Name = x.Hairdresser.Name,
                                        Address = x.Hairdresser.Address,
                                        PostalCode = x.Hairdresser.PostalCode,
                                        Phone = x.Hairdresser.Phone ?? "Sin número de teléfono"
                                  });
            return consulta.ToList();
        }

        public async Task<int> InsertHairdresserAsync(string name, string phone, string address, int postal_code, int user_id) {
            var newid = this.context.Hairdressers.Any() ? this.context.Hairdressers.Max(s => s.HairdresserId) + 1 : 1;
            Hairdresser hairdresser = new Hairdresser {
                HairdresserId = newid,
                Name = name,
                Phone = phone,
                Address = address,
                PostalCode = postal_code
            };
            this.context.Hairdressers.Add(hairdresser);

            await this.InsertAdminAsync(newid, user_id, AdminRole.Propietario);

            await this.context.SaveChangesAsync();
            return newid;
        }

        public async Task UpdateHairdresserAsync(int hairdresser_id, string name, string phone, string address, int postal_code) {
            Hairdresser? hairdresser = this.FindHairdresser(hairdresser_id);
            if (hairdresser != null) {
                hairdresser.Name = name;
                hairdresser.Phone = phone;
                hairdresser.Address = address;
                hairdresser.PostalCode = postal_code;
                await this.context.SaveChangesAsync();
            }
        }

        public async Task DeleteHairdresserAsync(int hairdresser_id) {
            Hairdresser? hairdresser = this.FindHairdresser(hairdresser_id);
            if (hairdresser != null) {
                this.context.Hairdressers.Remove(hairdresser);
                await this.context.SaveChangesAsync();
            }
        }
        #endregion

        #region SCHEDULE
        public List<string> GetNameSchedules(int hairdresser_id) {
            var consulta = from data in this.context.Schedules
                           where data.HairdresserId == hairdresser_id
                           select data.Name;
            return consulta.ToList();
        }

        public List<Schedule> GetSchedules(int hairdresser_id) {
            var consulta = from data in this.context.Schedules
                           where data.HairdresserId == hairdresser_id
                           select data;
            return consulta.ToList();
        }

        public Schedule? FindSchedule(int schedule_id) {
            var consulta = from data in this.context.Schedules
                           where data.ScheduleId == schedule_id
                           select data;
            return consulta.FirstOrDefault();
        }

        public async Task<int> InsertScheduleAsync(int hairdresser_id, string name, bool active) {
            if (active) {
                List<Schedule> schedules = this.GetSchedules(hairdresser_id);
                foreach (Schedule sch in schedules) {
                    if (sch.Active) { sch.Active = false; }
                }
            }

            var newid = this.context.Schedules.Any() ? this.context.Schedules.Max(s => s.ScheduleId) + 1 : 1;
            Schedule schedule = new Schedule {
                ScheduleId = newid,
                HairdresserId = hairdresser_id,
                Name = name,
                Active = active
            };

            this.context.Schedules.Add(schedule);
            await this.context.SaveChangesAsync();
            return newid;
        }

        public async Task UpdateScheduleAsync(int schedule_id, int hairdresser_id, string name, bool active) {
            Schedule? schedule = this.FindSchedule(schedule_id);
            if (schedule != null) {
                schedule.Name = name;
                schedule.Active = active;

                if (active) {
                    List<Schedule> schedules = this.GetSchedules(hairdresser_id);
                    foreach (Schedule sch in schedules) {
                        if (sch.ScheduleId != schedule_id && sch.Active) { sch.Active = false; }
                    }
                }

                await this.context.SaveChangesAsync();
            }
        }

        public async Task DeleteScheduleAsync(int schedule_id) {
            Schedule? schedule = this.FindSchedule(schedule_id);
            if (schedule != null) {
                this.context.Schedules.Remove(schedule);
                await this.context.SaveChangesAsync();
            }
        }
        #endregion

        #region SCHEDULE_ROW
        public List<Schedule_Row> GetScheduleRows(int schedule_id) {
            var consulta = from data in this.context.Schedule_Rows
                           where data.ScheduleId == schedule_id
                           select data;
            return consulta.ToList();
        }

        public Schedule_Row? FindScheduleRow(int schedule_row_id) {
            var consulta = from data in this.context.Schedule_Rows
                           where data.ScheduleRowId == schedule_row_id
                           select data;
            return consulta.FirstOrDefault();
        }

        private int ValidateScheduleRow(Schedule_Row schedule_row) {
            int schrow_compare = TimeSpan.Compare(schedule_row.Start, schedule_row.End);
            if (
                schrow_compare == 0 || schrow_compare == 1
            ) { return (int)Validates.Rango_incorrecto; }

            List<Schedule_Row> rows = this.GetScheduleRows(schedule_row.ScheduleId);
            foreach (Schedule_Row row in rows) {
                if ( // Validación de duplicados. Todos los valores tienen que ser iguales
                    row.ScheduleRowId != schedule_row.ScheduleRowId &&
                    row.ScheduleId == schedule_row.ScheduleId &&
                    row.Start == schedule_row.Start &&
                    row.End == schedule_row.End &&
                    row.Monday == schedule_row.Monday &&
                    row.Tuesday == schedule_row.Tuesday &&
                    row.Wednesday == schedule_row.Wednesday &&
                    row.Thursday == schedule_row.Thursday &&
                    row.Friday == schedule_row.Friday &&
                    row.Saturday == schedule_row.Saturday &&
                    row.Sunday == schedule_row.Sunday
                ) { return (int)Validates.Duplicado; }

                int start_compare_start = TimeSpan.Compare(schedule_row.Start, row.Start);
                int start_compare_end = TimeSpan.Compare(schedule_row.Start, row.End);
                int end_compare_start = TimeSpan.Compare(schedule_row.End, row.Start);
                int end_compare_end = TimeSpan.Compare(schedule_row.End, row.End);

                if (
                    row.ScheduleRowId != schedule_row.ScheduleRowId && (
                        start_compare_start == 0 ||                                                         // Los inicios igual al rango
                        (start_compare_start == 1 && start_compare_end == -1) ||                            // El inicio está entre los rangos
                        (start_compare_start == 1 && (end_compare_end == 0 || end_compare_end == -1)) ||    // Inicio correcto, final menor o igual al rango
                        (start_compare_start == -1 && (end_compare_start == 1 && end_compare_end == -1)) || // Inicio correcto, final entre rangos
                        (start_compare_start == -1 && (end_compare_end == 0 || end_compare_end == 1))       // Inicio correcto, final superior al rango
                    )
                ) { return (int)Validates.Rango_Sobreescrito; }
            }
            return (int)Validates.Ok;
        }

        public async Task<int> InsertScheduleRowsAsync(int schid, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun) {
            var newid = this.context.Schedule_Rows.Any() ? this.context.Schedule_Rows.Max(s => s.ScheduleRowId) + 1 : 1;
            Schedule_Row schedule_row = new Schedule_Row {
                ScheduleRowId = newid,
                ScheduleId = schid,
                Start = start,
                End = end,
                Monday = mon,
                Tuesday = tue,
                Wednesday = wed,
                Thursday = thu,
                Friday = fri,
                Saturday = sat,
                Sunday = sun
            };

            int validation = this.ValidateScheduleRow(schedule_row);
            if (validation == (int)Validates.Ok) { // Validación correcta
                this.context.Schedule_Rows.Add(schedule_row);
                await this.context.SaveChangesAsync();
            }
            return validation;
        }

        public async Task<int> UpdateScheduleRowsAsync(int schedule_row_id, TimeSpan start, TimeSpan end, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun) {
            Schedule_Row? schedule_row = this.FindScheduleRow(schedule_row_id);
            if (schedule_row != null) {
                schedule_row.Start = start;
                schedule_row.End = end;
                schedule_row.Monday = mon;
                schedule_row.Tuesday = tue;
                schedule_row.Wednesday = wed;
                schedule_row.Thursday = thu;
                schedule_row.Friday = fri;
                schedule_row.Saturday = sat;
                schedule_row.Sunday = sun;

                int validation = this.ValidateScheduleRow(schedule_row);
                if (validation == (int)Validates.Ok) { // Validación correcta
                    await this.context.SaveChangesAsync();
                }
                return validation;
            } else {
                return (int)Validates.No_Encontrado;
            }
        }

        public async Task DeleteScheduleRowsAsync(int schedule_row_id) {
            Schedule_Row? schedule_row = this.FindScheduleRow(schedule_row_id);
            if (schedule_row != null) {
                this.context.Schedule_Rows.Remove(schedule_row);
                await this.context.SaveChangesAsync();
            }
        }
        #endregion

    }
}
