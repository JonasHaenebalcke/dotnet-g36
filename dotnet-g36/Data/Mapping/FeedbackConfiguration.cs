﻿using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedback");
            builder.HasKey(f => f.FeedbackID);

            // effe in commentaar gezet
          /*  builder.HasOne(f => f.Auteur)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
