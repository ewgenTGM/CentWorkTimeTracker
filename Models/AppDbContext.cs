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
            User user = new User()
            {
                Id = 2,
                Email = "user@gmail.com",
                Name = "User",
                UserType = UserTypes.User,
                Password = "$2a$11$eCdOfxMCJCVOE3lT9j4zqeuTy732ixBNV1FtvzNVr0AcHZ66TkTMm"
            };

            Vacation vacation = new Vacation()
            {
                Id = 1,
                UserId = 2,
                From = new DateTime(2021, 01, 01),
                To = new DateTime(2021, 01, 15),
                Type = VacationType.Paid,
                Description = "Просто захотел погулять"
            };

            Sick sick = new Sick()
            {
                Id = 1,
                UserId = 2,
                From = new DateTime(2021, 01, 01),
                To = new DateTime(2021, 01, 15),
                Type = SickType.Leave,
                Description = "Зобелел волчанкой"
            };

            Transfer transfer = new Transfer()
            {
                Id = 1,
                UserId = 2,
                From = new DateTime(2021, 10, 11),
                To = new DateTime(2021, 10, 16),
                Description = "Пойду по бабам, " +
                "отработа" +
                "ю в субботу без баб"
            };

            modelBuilder.Entity<User>().HasData(new[] { manager, user });
            modelBuilder.Entity<Vacation>().HasData(vacation);
            modelBuilder.Entity<Transfer>().HasData(transfer);
            modelBuilder.Entity<Sick>().HasData(sick);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Sick> Sicks { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}