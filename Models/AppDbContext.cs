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

            Claim vacation = new Claim()
            {
                Id = 1,
                UserId = 2,
                From = new DateTime(2021, 01, 01),
                To = new DateTime(2021, 01, 15),
                Type = ClaimType.Vacation,
                Description = "Поеду на море"
            };

            Claim unpaidedVacation = new Claim()
            {
                Id = 2,
                UserId = 2,
                From = new DateTime(2021, 01, 01),
                To = new DateTime(2021, 01, 15),
                Type = ClaimType.Vacation,
                Description = "Нужно закопать тело в лесу"
            };

            Claim sick = new Claim()
            {
                Id = 3,
                UserId = 2,
                From = new DateTime(2021, 01, 01),
                To = new DateTime(2021, 01, 15),
                Type = ClaimType.Sick,
                Description = "Зобелел волчанкой, справка есть"
            };

            Claim sickDays = new Claim()
            {
                Id = 4,
                UserId = 2,
                From = new DateTime(2021, 01, 01),
                To = new DateTime(2021, 01, 15),
                Type = ClaimType.SickDays,
                Description = "Отморозил нос"
            };

            Claim transfer = new Claim()
            {
                Id = 5,
                UserId = 2,
                From = new DateTime(2021, 10, 11),
                To = new DateTime(2021, 10, 16),
                Type = ClaimType.Transfer,
                Description = "Пойду по бабам, " +
                "отработа" +
                "ю в субботу без баб"
            };

            modelBuilder.Entity<User>().HasData(new[] { manager, user });
            modelBuilder.Entity<Claim>().HasData(new[] { vacation, unpaidedVacation, sick, sickDays, transfer });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
    }
}