using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace yujvidya.Migrations
{
    public partial class MoreModelClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AltMobileNumber",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BatchType",
                table: "Persons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Amount = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    PreferredBatch = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileNumbers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "MobileNumbers");

            migrationBuilder.DropColumn(
                name: "AltMobileNumber",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "BatchType",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "Persons");
        }
    }
}
