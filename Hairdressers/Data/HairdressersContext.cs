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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Admin>()
                .HasKey(a => new { a.HairdresserId, a.UserId });

            modelBuilder.Entity<Schedule>()
                .HasMany(s => s.ScheduleRows)
                .WithOne(s => s.Schedule)
                .HasForeignKey(sr => sr.ScheduleId);
        }

    }
}
