using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvermoreBakery.Service
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string connectionString;
        public DbSet<Users> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Role { get; set; }

        public ApplicationDbContext()
        {
            Env.TraversePath().Load();
            string dbHost = Env.GetString("DB_HOST");
            string dbPort = Env.GetString("DB_PORT");
            string dbUser = Env.GetString("DB_USER");
            string dbPass = Env.GetString("DB_PASS");
            string dbName = Env.GetString("DB_NAME");

            connectionString =
                $"Server={dbHost}; " +
                $"Port={dbPort}; " +
                $"User ID={dbUser}; " +
                $"Password={dbPass}; " +
                $"Database={dbName}";

            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while configuring the database: {ex.Message}");
                throw;
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
