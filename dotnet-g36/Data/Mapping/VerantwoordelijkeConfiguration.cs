using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace dotnet_g36.Data.Mapping
{
    public class VerantwoordelijkeConfiguration : IEntityTypeConfiguration<Verantwoordelijke>
    {
        public void Configure(EntityTypeBuilder<Verantwoordelijke> builder)
        {
            builder.ToTable("Verantwoordelijke");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Voornaam).IsRequired();
            builder.Property(u => u.Familienaam).IsRequired();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Email).IsRequired();

            builder.HasMany(v => v.OpenTeZettenSessies)
                .WithOne(s => s.Verantwoordelijke)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
