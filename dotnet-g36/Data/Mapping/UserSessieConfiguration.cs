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
                
    
            builder.HasOne(s => s.Sessie)
                .WithMany(s => s.UserSessies)
                .HasForeignKey(us => us.SessieID);

            builder.HasOne(s => s.User)
                .WithMany(s => s.UserSessies)
                .HasForeignKey(us => us.UserID);
        }
    }
}
