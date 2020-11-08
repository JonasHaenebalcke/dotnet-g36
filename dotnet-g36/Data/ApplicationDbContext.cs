using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Data.Mapping;
using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_g36.Data
{
    public class ApplicationDbContext : IdentityDbContext<Gebruiker, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
                {
                }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"ConnectionDetails";optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Sessie> Sessies { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Verantwoordelijke> Verantwoordelijken { get; set; }
        public DbSet<Verantwoordelijke> Hoofdverantwoordelijke { get; set; }
        public DbSet<GebruikerSessie> GebruikerSessies { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
               
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SessieConfiguration());
            modelBuilder.ApplyConfiguration(new GebruikerConfiguration());
            modelBuilder.ApplyConfiguration(new GebruikerSessieConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
            modelBuilder.Entity<Verantwoordelijke>().HasBaseType<Gebruiker>();

        }
    }
}
