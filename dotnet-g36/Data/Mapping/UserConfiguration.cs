using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class UserConfiguration : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Voornaam).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Familienaam).HasMaxLength(20).IsRequired();
            builder.Property(u => u.UserName).HasMaxLength(15).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();


            //Dit is voor de overevering, er hoort nu
            //een extra kolom 'type' te komen met gepaste waarde in
            builder.HasDiscriminator<string>("Type")
                .HasValue<Verantwoordelijke>("Verantwoordelijke");
        }
    }
}
