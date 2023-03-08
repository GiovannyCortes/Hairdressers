using Hairdressers.Data;
using Hairdressers.Interfaces;
using Hairdressers.Models;

namespace Hairdressers.Repositories {

    public enum Validates { No_Encontrado = -1, Ok = 0, Rango_Sobreescrito = 1, Duplicado = 2, Rango_incorrecto = 3 }

    public class RepositoryScheduleRow : IRepositoryScheduleRow {

        private HairdressersContext context;

        public RepositoryScheduleRow(HairdressersContext context) {
            this.context = context;
        }

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

    }
}
