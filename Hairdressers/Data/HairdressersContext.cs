﻿using Hairdressers.Models;
using Microsoft.EntityFrameworkCore;

namespace Hairdressers.Data {
    public class HairdressersContext : DbContext {

        public HairdressersContext(DbContextOptions<HairdressersContext> options) :base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Hairdresser> Hairdressers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Admin>()
                .HasKey(a => new { a.HairdresserId, a.UserId });
        }

    }
}