using Hairdressers.Data;
using Hairdressers.Interfaces;
using Hairdressers.Models;
using System.Xml.Linq;
using System;

namespace Hairdressers.Repositories {
    public class RepositorySchedule : IRepositorySchedule {

        private HairdressersContext context;

        public RepositorySchedule(HairdressersContext context) {
            this.context = context;
        }

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
    }
}