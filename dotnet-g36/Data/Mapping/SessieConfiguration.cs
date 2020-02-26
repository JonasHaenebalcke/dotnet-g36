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
            builder.ToTable("Sessie");

            builder.HasKey(s => s.SessieID);
            builder.HasOne(s => s.Verantwoordelijke)
                .WithMany(s => s.Sessies)
                .IsRequired(false);

            builder.HasOne(s => s.Hoofdverantwoordelijke)
                .WithMany(s => s.Sessies)
                .IsRequired(false);



        }
    }
}
