using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using yujvidya.Models;
using Hangfire.PostgreSql;
using Hangfire;

namespace yujvidya
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PersonDetails> PersonDetails { get; set; }

        public DbSet<PersonCareTaker> PersonCareTakes { get; set; }

        public DbSet<MobileNumber> MobileNumbers { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<EnrollmentType> EnrollmentTypes { get; set; }

        public DbSet<BatchSchedule> BatchSchedules { get; set; }

        public DbSet<DueDateNotification> DueDateNotifications { get; set; }

        public DbSet<SmsDetail> SmsDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseNpgsql(ConfigStrings.DbConnectionString);
        }
    }
}