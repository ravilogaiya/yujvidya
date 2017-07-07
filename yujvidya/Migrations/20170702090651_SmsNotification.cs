using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace yujvidya.Migrations
{
    public partial class SmsNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "Persons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DueDateNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    EnrollmentId = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    SmsDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DueDateNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Message = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DueDateNotifications");

            migrationBuilder.DropTable(
                name: "SmsDetails");

            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "Persons");
        }
    }
}
