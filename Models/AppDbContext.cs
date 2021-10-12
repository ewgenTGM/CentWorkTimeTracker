using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User manager = new User()
            {
                Id = 1,
                Email = "manager.cent@gmail.com",
                Name = "Manager",
                UserType = UserTypes.Manager,
                Password = "$2a$11$eCdOfxMCJCVOE3lT9j4zqeuTy732ixBNV1FtvzNVr0AcHZ66TkTMm"
            };
            modelBuilder.Entity<User>().HasData(manager);
        }

        public DbSet<User> Users { get; set; }
    }
}