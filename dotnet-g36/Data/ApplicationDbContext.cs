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
           var connectionString = @"Server=40.68.196.74;Database=ItLabTest;user id=jonas.haenebalcke;password=5e39b@2D8905;Integrated Security=True;Trusted_Connection=False;";
          // var connectionString = @"Server=.;Database=ItLabTest;Integrated Security=True;";
            //var connectionString = @"Server=.,1433;Database=ItLabTest;User Id=SA;Password=Rein1234;Integrated Security=True;Trusted_Connection=False;";
            //var connectionString = @"Server=40.68.196.74;Database=ItLabTest;User Id=lucas.vanderhaegen;Password=57eC@2e81d57;Integrated Security=True;Trusted_Connection=False;";
            optionsBuilder.UseSqlServer(connectionString);
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
