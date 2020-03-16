using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class GebruikerConfiguration : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
            builder.ToTable("Gebruiker");
            builder.Property(u => u.Barcode).HasMaxLength(50);
            builder.Property(u => u.Voornaam).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Familienaam).HasMaxLength(50).IsRequired();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.Email).IsRequired();

            //Dit is voor de overevering, er hoort nu
            //een extra kolom 'type' te komen met gepaste waarde in
            builder.HasDiscriminator<string>("Type")
                .HasValue<Verantwoordelijke>("Verantwoordelijke");
        }
    }
}
