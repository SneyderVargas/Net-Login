using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SmartFishLogin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.Infra
{
    public class DefaultDbContext : IdentityDbContext<UsersEntity, RoleEntity, string>
    {
        private string _connectionString;
        public DefaultDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbContextDefault");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserEntity.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(GetConnection());
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
        public DbSet<UserEntity> userEntities { get; set; }
    }
}
