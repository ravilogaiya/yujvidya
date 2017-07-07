using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using yujvidya;

namespace yujvidya.Migrations
{
    [DbContext(typeof(PersonContext))]
    [Migration("20170520175538_EnrollmentTypeBatchTimeChange")]
    partial class EnrollmentTypeBatchTimeChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("yujvidya.BatchSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Days");

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

                    b.Property<double>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<int>("PersonId");

                    b.Property<int?>("PreferredBatchId");

                    b.Property<bool>("SentPaymentAcknowledgement");

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("PreferredBatchId");

                    b.HasIndex("TypeId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("yujvidya.EnrollmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTime>("FromDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("EnrollmentTypes");
                });

            modelBuilder.Entity("yujvidya.MobileNumber", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MobileNumbers");
                });

            modelBuilder.Entity("yujvidya.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AltMobileNumber");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FatherName");

                    b.Property<int>("Gender");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("MotherName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("yujvidya.Enrollment", b =>
                {
                    b.HasOne("yujvidya.BatchSchedule", "PreferredBatch")
                        .WithMany()
                        .HasForeignKey("PreferredBatchId");

                    b.HasOne("yujvidya.EnrollmentType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });
        }
    }
}
