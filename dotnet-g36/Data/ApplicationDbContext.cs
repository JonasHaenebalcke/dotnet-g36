using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Data.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_g36.Data
{
    public class ApplicationDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=.;Database=ItLabTest;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Deelnemer> Deelnemers { get; set; }
        public DbSet<Hoofdverantwoordelijke> Hoofdverantwoordelijken { get; set; }
        public DbSet<Sessie> Sessies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Verantwoordelijke> Verantwoordelijken { get; set; }
        public DbSet<UserSessie> UserSessies { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SessieConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSessieConfiguration());
            modelBuilder.Entity<Deelnemer>().HasBaseType<User>();
            modelBuilder.Entity<Hoofdverantwoordelijke>().HasBaseType<User>();
            modelBuilder.Entity<Verantwoordelijke>().HasBaseType<User>();



        }
        
    }
}
