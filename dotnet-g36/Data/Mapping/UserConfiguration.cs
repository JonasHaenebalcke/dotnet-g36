﻿using dotnet_g36.Models.Domain;
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
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.Voornaam).IsRequired();
            builder.Property(u => u.Familienaam).IsRequired();
            //builder.Property(u => u.GebruikersNaam).IsRequired();


            //Dit is voor de overevering, er hoort nu
            //een extra kolom 'type' te komen met gepaste waarde in
            builder.HasDiscriminator<String>("Type")
                .HasValue<Verantwoordelijke>("Verantwoordelijke");
        }
    }
}
