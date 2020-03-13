using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace dotnet_g36.Data.Mapping
{
    public class VerantwoordelijkeConfiguration : IEntityTypeConfiguration<Verantwoordelijke>
    {
        public void Configure(EntityTypeBuilder<Verantwoordelijke> builder)
        {
            //builder.ToTable("Verantwoordelijke");
            //builder.HasKey(u => u.Barcode);
            /*builder.Property(u => u.Voornaam).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Familienaam).HasMaxLength(20).IsRequired();
            builder.Property(u => u.UserName)/*.HasMaxLength(15).IsRequired();
            builder.Property(u => u.PasswordHash)/*.HasMaxLength(100).IsRequired();
            builder.Property(u => u.Email)/*.HasMaxLength(50).IsRequired();*/
            builder.Property(u => u.IsHoofdverantwoordelijke).IsRequired();

            builder.HasMany(v => v.OpenTeZettenSessies)
                .WithOne(s => s.Verantwoordelijke)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
