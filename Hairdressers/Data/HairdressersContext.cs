using Hairdressers.Models;
using Microsoft.EntityFrameworkCore;

namespace Hairdressers.Data {
    public class HairdressersContext : DbContext {

        public HairdressersContext(DbContextOptions<HairdressersContext> options) :base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Hairdresser> Hairdressers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Schedule_Row> Schedule_Rows { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment_Service> AppointmentServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Admin>()
                .HasKey(a => new { a.HairdresserId, a.UserId });
            
            modelBuilder.Entity<Appointment_Service>()
                .HasKey(a => new { a.AppointmentId, a.ServiceId });

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(5, 2);

        }

    }
}
