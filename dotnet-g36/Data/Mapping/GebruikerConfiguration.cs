﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class GebruikerConfiguration : IEntityTypeConfiguration<Deelnemer>
    {
        public void Configure(EntityTypeBuilder<Deelnemer> builder)
        {
            builder.ToTable("Deelnemer");
        }
    }
}