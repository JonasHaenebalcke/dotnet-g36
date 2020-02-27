using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class VerantwoordelijkeConfiguration : IEntityTypeConfiguration<Verantwoordelijke>
    {
        public void Configure(EntityTypeBuilder<Verantwoordelijke> builder)
        {
            builder.ToTable("Verantwoordelijke");

            builder.HasMany(v => v.GeorganiseerdeSessies)
                .WithOne(s => s.Verantwoordelijke);
        }
    }
}
