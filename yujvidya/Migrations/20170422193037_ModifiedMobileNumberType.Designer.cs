using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using yujvidya;

namespace yujvidya.Migrations
{
    [DbContext(typeof(PersonContext))]
    [Migration("20170422193037_ModifiedMobileNumberType")]
    partial class ModifiedMobileNumberType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("yujvidya.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<int>("PersonId");

                    b.Property<int>("PreferredBatch");

                    b.Property<bool>("SentPaymentAcknowledgement");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Enrollments");
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

                    b.Property<int>("BatchType");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FatherName");

                    b.Property<int>("Gender");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("MotherName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });
        }
    }
}
