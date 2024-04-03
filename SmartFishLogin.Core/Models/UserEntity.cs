using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? TypeUsers { get; set; }  // Default value of 'null' might need handling
        public string? Data { get; set; }
        public DateTime? LastLogin { get; set; } // Nullable DateTime for potential null values
        public int? Active { get; set; }
        public DateTime? CreateRegisterDate { get; set; } // Nullable DateTime for potential null values
        public DateTime? UpdateRegisterDate { get; set; } // Nullable DateTime for potential null values
        public int? ActiveRegister { get; set; }
        public int? Tenancies { get; set; }  // Assuming tenancys is an integer based on the schema
        public string? Steps { get; set; }
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserEntity>();
            entity.ToTable("users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Tenancies)
              .HasColumnName("tenancys");

        }
    }
}
