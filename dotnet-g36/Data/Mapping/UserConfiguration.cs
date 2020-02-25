using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            //Dit is voor de overevering, er hoort nu
            //een extra kolom 'type' te komen met gepaste waarde in
            builder.HasDiscriminator<String>("Type")
                .HasValue("Gebuiker")
                .HasValue("Verantwoordelijke")
                .HasValue("Hoofdverantwoordelijke");
        }
    }
}
