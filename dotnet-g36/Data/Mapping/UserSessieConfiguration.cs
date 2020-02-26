using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Mapping
{
    public class UserSessieConfiguration : IEntityTypeConfiguration<UserSessie>
    {
        public void Configure(EntityTypeBuilder<UserSessie> builder)
        {
            builder.ToTable("UserSessie");

            builder.HasKey(us => new { us.SessieID, us.UserID });
                
            builder.HasOne(u => u.User)
                 .WithMany(us => us.IngeschrevenSessies)
                 .HasForeignKey(us => us.UserID);
            builder.HasOne(u => u.User)
                 .WithMany(us => us.AanwezigeSessies)
                 .HasForeignKey(us => us.UserID);

            builder.HasOne(s => s.Sessie)
                .WithMany(us => us.Aanwezigen)
                .HasForeignKey(us => us.SessieID);
            builder.HasOne(s => s.Sessie)
                .WithMany(us => us.Ingeschreven)
                .HasForeignKey(us => us.SessieID);

        }
    }
}
