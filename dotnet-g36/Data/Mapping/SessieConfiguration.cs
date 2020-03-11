using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class SessieConfiguration : IEntityTypeConfiguration<Sessie>
    {
        public void Configure(EntityTypeBuilder<Sessie> builder)
        {
            //Autogenerate ID nummer? UserSessies van kolom naam veranderen?
            builder.ToTable("Sessie");
            builder.HasKey(s => s.SessieID);
            builder.Property(s => s.Lokaal).IsRequired();
            builder.Property(s => s.StartDatum).IsRequired();
            builder.Property(s => s.EindDatum).IsRequired();
            builder.Property(s => s.Titel).IsRequired();


            builder.HasOne(s => s.Verantwoordelijke)
                .WithMany(s => s.OpenTeZettenSessies)
                .IsRequired(true)
                 .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(s => s.Hoofdverantwoordelijke)
            //    .WithMany(s => s.OpenTeZettenSessies)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.FeedbackList)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
