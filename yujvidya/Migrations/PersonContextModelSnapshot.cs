using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using yujvidya;

namespace yujvidya.Migrations
{
    [DbContext(typeof(PersonContext))]
    partial class PersonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("yujvidya.BatchSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Days");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("BatchSchedules");
                });

            modelBuilder.Entity("yujvidya.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AcknowledgementSent");

                    b.Property<double>("Amount");

                    b.Property<int>("EnrollmentTypeId");

                    b.Property<DateTime>("FromDate");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<int>("PersonId");

                    b.Property<int>("PreferredBatchScheduleId");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentTypeId");

                    b.HasIndex("PreferredBatchScheduleId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("yujvidya.EnrollmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<bool>("Deleted");

                    b.Property<int>("Duration");

                    b.Property<int>("DurationType");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("EnrollmentTypes");
                });

            modelBuilder.Entity("yujvidya.MobileNumber", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("MobileNumbers");
                });

            modelBuilder.Entity("yujvidya.Models.DueDateNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("EnrollmentId");

                    b.Property<int>("Level");

                    b.Property<int>("PersonId");

                    b.Property<int>("SmsDetailId");

                    b.HasKey("Id");

                    b.ToTable("DueDateNotifications");
                });

            modelBuilder.Entity("yujvidya.Models.SmsDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<string>("MobileNumber");

                    b.Property<int>("PersonId");

                    b.Property<int>("Status");

                    b.Property<string>("StatusDescription");

                    b.HasKey("Id");

                    b.ToTable("SmsDetails");
                });

            modelBuilder.Entity("yujvidya.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<bool>("Inactive");

                    b.Property<string>("LastName");

                    b.Property<string>("MobileNumber");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("yujvidya.PersonCareTaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MobileNumber");

                    b.Property<int>("PersonId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("PersonCareTakes");
                });

            modelBuilder.Entity("yujvidya.PersonDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Comments");

                    b.Property<DateTime>("Date");

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.ToTable("PersonDetails");
                });

            modelBuilder.Entity("yujvidya.Enrollment", b =>
                {
                    b.HasOne("yujvidya.EnrollmentType", "Type")
                        .WithMany()
                        .HasForeignKey("EnrollmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("yujvidya.BatchSchedule", "PreferredBatch")
                        .WithMany()
                        .HasForeignKey("PreferredBatchScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
