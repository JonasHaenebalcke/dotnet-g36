using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace dotnet_g36.Data.Mapping
{
    public class VerantwoordelijkeConfiguration : IEntityTypeConfiguration<Verantwoordelijke>
    {
        public void Configure(EntityTypeBuilder<Verantwoordelijke> builder)
        {
            builder.Property(u => u.IsHoofdverantwoordelijke).IsRequired();
        }
    }
}
