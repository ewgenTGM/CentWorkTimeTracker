using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Guid managerGuid = new Guid();
            //Guid userGuid = new Guid();
            //AppUser manager = new AppUser()
            //{
            //    Id = managerGuid.ToString(),
            //    Email = "manager.cent@gmail.com",
            //    UserName = "Manager",
            //    UserType = UserTypes.Manager,
            //    Ha = "$2a$11$eCdOfxMCJCVOE3lT9j4zqeuTy732ixBNV1FtvzNVr0AcHZ66TkTMm"
            //};
            //AppUser user = new AppUser()
            //{
            //    Id = 2,
            //    Email = "user@gmail.com",
            //    Name = "User",
            //    UserType = UserTypes.User,
            //    Password = "$2a$11$eCdOfxMCJCVOE3lT9j4zqeuTy732ixBNV1FtvzNVr0AcHZ66TkTMm"
            //};

            //Vacation vacation = new Vacation()
            //{
            //    Id = new Guid(),
            //    UserId = 2,
            //    DateBegin = new DateTime(2021, 01, 01),
            //    DateEnd = new DateTime(2021, 01, 15),
            //};

            //UnpaidedVacation unpaidedVacation = new UnpaidedVacation()
            //{
            //    Id = new Guid(),
            //    UserId = 2,
            //    DateBegin = new DateTime(2021, 01, 01),
            //    DateEnd = new DateTime(2021, 01, 15),
            //    Description = "Нужно закопать тело в лесу"
            //};

            //Sick sick = new Sick()
            //{
            //    Id = new Guid(),
            //    UserId = 2,
            //    DateBegin = new DateTime(2021, 01, 01),
            //    DateEnd = new DateTime(2021, 01, 15),
            //    DocNumber = "БН12321"
            //};
            //sick.Approve();

            //SickDays sickDays = new SickDays()
            //{
            //    Id = new Guid(),
            //    UserId = 2,
            //    DateBegin = new DateTime(2021, 01, 01),
            //    DateEnd = new DateTime(2021, 01, 15),
            //    Description = "Болят волосы"
            //};

            //Transfer transfer = new Transfer()
            //{
            //    Id = new Guid(),
            //    UserId = 2,
            //    DayFrom = new DateTime(2021, 10, 11),
            //    DayTo = new DateTime(2021, 10, 16),
            //    Description = "Пойду по бабам, " +
            //    "отработа" +
            //    "ю в субботу без баб"
            //};
            //transfer.Reject();

            //modelBuilder.Entity<User>().HasData(new[] { manager, user });
            //modelBuilder.Entity<Vacation>().HasData(vacation);
            //modelBuilder.Entity<Sick>().HasData(sick);
            //modelBuilder.Entity<SickDays>().HasData(sickDays);
            //modelBuilder.Entity<Transfer>().HasData(transfer);
            //modelBuilder.Entity<UnpaidedVacation>().HasData(unpaidedVacation);
        }

        //public DbSet<User> Users { get; set; }
        AppUser admin = new AppUser();

        

        public DbSet<Request> Requests { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Sick> Sicks { get; set; }
        public DbSet<SickDays> SickDays { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<WorkFromHome> WorkFromHomes { get; set; }
        public DbSet<UnpaidedVacation> UnpaidedVacations { get; set; }
    }
}