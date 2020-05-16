using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class AankondigingConfiguration : IEntityTypeConfiguration<Aankondiging>
    {
        /* public void Configure(EntityTypeBuilder<SessieKalender> builder)
         {
             builder.ToTable("SessieKalender");
             builder.HasKey(s => s.SessieKalenderID);
             builder.Property(s => s.StartDatum).IsRequired();
             builder.Property(s => s.EindDatum).IsRequired();

             builder.HasMany(s => s.Sessies)
                 .WithOne(s => s.SessieKalender)
                 .OnDelete(DeleteBehavior.Restrict);
         }*/
        public void Configure(EntityTypeBuilder<Aankondiging> builder)
        {
            builder.ToTable("Aankondiging");
            builder.HasKey(a => a.AankondigingID);
            builder.Property(a => a.DatumAangemaakt);
            builder.Property(a => a.IsVerzonden);
            builder.Property(a => a.Tekst);
            builder.Property(a => a.Titel);

            builder.HasOne(a => a.Publicist)
                .WithMany(g => g.Aankondigingen);

            builder.HasOne(a => a.Sessie)
                .WithMany(s => s.Aankondigingen);
        }
    }
}
