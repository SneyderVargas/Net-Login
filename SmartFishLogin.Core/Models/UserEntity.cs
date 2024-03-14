using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Models
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public bool IsLoggedIn { get; set; }
        public DateTime LogInTimeout { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string NumDocument { get; set; }
        public long fk_cargo { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity => entity.Property(p => p.Id).HasMaxLength(128));
            modelBuilder.Entity<UserEntity>(entity => entity.Property(p => p.NormalizedEmail).HasMaxLength(128));
            modelBuilder.Entity<UserEntity>(entity => entity.Property(p => p.NormalizedUserName).HasMaxLength(128));

            modelBuilder.Entity<UserEntity>(entity => entity.Property(p => p.Active).HasDefaultValue(true));
            modelBuilder.Entity<UserEntity>(entity => entity.Property(p => p.IsLoggedIn).HasDefaultValue(false));
            modelBuilder.Entity<UserEntity>(entity => entity.Property(p => p.LogInTimeout).HasDefaultValue(DateTime.MinValue));
            modelBuilder.Entity<UserEntity>(entity => entity.Property(p => p.NumDocument).HasMaxLength(128));

            modelBuilder.Entity<RoleEntity>(entity => entity.Property(p => p.Id).HasMaxLength(128));
            modelBuilder.Entity<RoleEntity>(entity => entity.Property(p => p.NormalizedName).HasMaxLength(128));

            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(p => p.LoginProvider).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(p => p.UserId).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(p => p.Name).HasMaxLength(128));

            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(p => p.UserId).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(p => p.RoleId).HasMaxLength(128));

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(p => p.LoginProvider).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(p => p.ProviderKey).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(p => p.UserId).HasMaxLength(128));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(p => p.Id).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(p => p.UserId).HasMaxLength(128));

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(p => p.Id).HasMaxLength(128));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(p => p.RoleId).HasMaxLength(128));
        }
    }
}
