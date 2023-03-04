using Hairdressers.Models;
using Microsoft.EntityFrameworkCore;

namespace Hairdressers.Data {
    public class HairdressersContext : DbContext {

        public HairdressersContext(DbContextOptions<HairdressersContext> options) :base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
