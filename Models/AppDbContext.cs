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
                DateBegin = new DateTime(2021, 01, 01),
                DateEnd = new DateTime(2021, 01, 15),
            };

            UnpaidedVacation unpaidedVacation = new UnpaidedVacation()
            {
                Id = 2,
                UserId = 2,
                DateBegin = new DateTime(2021, 01, 01),
                DateEnd = new DateTime(2021, 01, 15),
                Description = "Нужно закопать тело в лесу"
            };

            Sick sick = new Sick()
            {
                Id = 3,
                UserId = 2,
                DateBegin = new DateTime(2021, 01, 01),
                DateEnd = new DateTime(2021, 01, 15),
                DocNumber = "БН12321"
            };
            sick.Approve();

            SickDays sickDays = new SickDays()
            {
                Id = 4,
                UserId = 2,
                DateBegin = new DateTime(2021, 01, 01),
                DateEnd = new DateTime(2021, 01, 15),
                Description = "Болят волосы"
            };

            Transfer transfer = new Transfer()
            {
                Id = 5,
                UserId = 2,
                DayFrom = new DateTime(2021, 10, 11),
                DayTo = new DateTime(2021, 10, 16),
                Description = "Пойду по бабам, " +
                "отработа" +
                "ю в субботу без баб"
            };
            transfer.Reject();

            modelBuilder.Entity<User>().HasData(new[] { manager, user });
            modelBuilder.Entity<Vacation>().HasData(vacation);
            modelBuilder.Entity<Sick>().HasData(sick);
            modelBuilder.Entity<SickDays>().HasData(sickDays);
            modelBuilder.Entity<Transfer>().HasData(transfer);
            modelBuilder.Entity<UnpaidedVacation>().HasData(unpaidedVacation);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Sick> Sicks { get; set; }
        public DbSet<SickDays> SickDays { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<WorkFromHome> WorkFromHomes { get; set; }
        public DbSet<UnpaidedVacation> UnpaidedVacations { get; set; }
    }
}