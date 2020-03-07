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

            builder.HasMany(v => v.OpenTeZettenSessies)
                .WithOne(s => s.Verantwoordelijke)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasDiscriminator<String>("Type")
            //    .HasValue<Hoofdverantwoordelijke>("Hoofdverantwoordelijke");

        }
    }
}
