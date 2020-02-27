using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class HoofdverantwoordelijkeConfiguration : IEntityTypeConfiguration<Hoofdverantwoordelijke>
    {
        public void Configure(EntityTypeBuilder<Hoofdverantwoordelijke> builder)
        {
            builder.ToTable("Hoofdverantwoordelijke");

            builder.HasMany(v => v.AlleSessies)
                .WithOne(v => v.Hoofdverantwoordelijke);
        }
    }
}
