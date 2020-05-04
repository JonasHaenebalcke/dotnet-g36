using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class SessieKalenderConfiguration : IEntityTypeConfiguration<SessieKalender>
    {
        public void Configure(EntityTypeBuilder<SessieKalender> builder)
        {
            builder.ToTable("SessieKalender");
            builder.HasKey(s => s.SessieKalenderID);
            builder.Property(s => s.StartDatum).IsRequired();
            builder.Property(s => s.EindDatum).IsRequired();

            builder.HasMany(s => s.Sessies)
                .WithOne(s => s.SessieKalender)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
