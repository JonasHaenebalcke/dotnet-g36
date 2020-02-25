using dotnet_g36.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class SessieHoofdverantwoordelijkeConfiguration : IEntityTypeConfiguration<SessieHoofdverantwoordelijke>
    {
        public void Configure(EntityTypeBuilder<SessieHoofdverantwoordelijke> builder)
        {
            builder.ToTable("SessieHoofdverantwoordelijke");

            builder.HasOne(sh => sh.Sessie)
                .WithMany(s => s.SessieHoofdverantwoordelijke)
                .HasForeignKey(sh => sh.SessieID);

            builder.HasOne(sh => sh.Hoofdverantwoordelijke)
              .WithMany(s => s.SessieHoofdverantwoordelijke)
              .HasForeignKey(sh => sh.HoofdverantwoordelijkeID);
        }
    }
}
