using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Core.Models
{
    public class RolesEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int ActiveRegister { get; set; }

        public int Tenancys { get; set; }
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<RolesEntity>();

            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActiveRegister)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activeRegister");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(254)
                .IsFixedLength()
                .HasColumnName("descripcion");
            entity.Property(e => e.Name)
                .HasMaxLength(254)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Tenancys).HasColumnName("tenancys");
        }
    }
}
