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

            //Moet veranderd worden naar verantwoordelijke object
            builder.HasOne(s => s.NaamVerantwoordelijke)
                .WithMany(s => s.sessie);//Verantwoordelijke heeft een sessie

            //Een sessie kan verantwoordelijke of hoofdverantwoordelijke hebben
            builder.HasOne(s => s.NaamVerantwoordelijke)
                .WithMany(s => s.sessie);


        }
    }
}
