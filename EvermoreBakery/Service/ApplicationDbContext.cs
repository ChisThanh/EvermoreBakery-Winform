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
        static readonly string connectionString = 
            "Server=localhost; " +
            "Port=3307; " +
            "User ID=root; " +
            "Password=root1; " +
            "Database=EvermoreBakery";

        public DbSet<Users> Users { get; set; }

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
