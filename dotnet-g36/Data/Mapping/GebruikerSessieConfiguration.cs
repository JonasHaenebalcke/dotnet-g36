using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class GebruikerSessieConfiguration : IEntityTypeConfiguration<GebruikerSessie>
    {
        public void Configure(EntityTypeBuilder<GebruikerSessie> builder)
        {
            builder.ToTable("GebruikerSessie");

            builder.HasKey(us => new { us.SessieID, us.GebruikerID});
            builder.Property(us => us.SessieID).IsRequired();
            builder.Property(us => us.GebruikerID).IsRequired();

            builder.HasOne(s => s.Sessie)
                .WithMany(s => s.GebruikerSessies)
                .HasForeignKey(us => us.SessieID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Gebruiker)
                .WithMany(s => s.GebruikerSessies)
                .HasForeignKey(us => us.GebruikerID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
