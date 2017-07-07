using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace yujvidya.Migrations
{
    public partial class RedefinedRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_BatchSchedules_PreferredBatchId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_EnrollmentTypes_TypeId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_PreferredBatchId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_TypeId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "AltMobileNumber",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PreferredBatchId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "MotherName",
                table: "Persons",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MobileNumbers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "SentPaymentAcknowledgement",
                table: "Enrollments",
                newName: "AcknowledgementSent");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Enrollments",
                newName: "ToDate");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "MobileNumbers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "EnrollmentTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentTypeId",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PreferredBatchScheduleId",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "BatchSchedules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PersonCareTakes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCareTakes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Comments = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_EnrollmentTypeId",
                table: "Enrollments",
                column: "EnrollmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_PreferredBatchScheduleId",
                table: "Enrollments",
                column: "PreferredBatchScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_EnrollmentTypes_EnrollmentTypeId",
                table: "Enrollments",
                column: "EnrollmentTypeId",
                principalTable: "EnrollmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_BatchSchedules_PreferredBatchScheduleId",
                table: "Enrollments",
                column: "PreferredBatchScheduleId",
                principalTable: "BatchSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_EnrollmentTypes_EnrollmentTypeId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_BatchSchedules_PreferredBatchScheduleId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "PersonCareTakes");

            migrationBuilder.DropTable(
                name: "PersonDetails");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_EnrollmentTypeId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_PreferredBatchScheduleId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "MobileNumbers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "EnrollmentTypes");

            migrationBuilder.DropColumn(
                name: "EnrollmentTypeId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "PreferredBatchScheduleId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "BatchSchedules");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Persons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Persons",
                newName: "MotherName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "MobileNumbers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "Enrollments",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "AcknowledgementSent",
                table: "Enrollments",
                newName: "SentPaymentAcknowledgement");

            migrationBuilder.AddColumn<string>(
                name: "AltMobileNumber",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferredBatchId",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_PreferredBatchId",
                table: "Enrollments",
                column: "PreferredBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TypeId",
                table: "Enrollments",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_BatchSchedules_PreferredBatchId",
                table: "Enrollments",
                column: "PreferredBatchId",
                principalTable: "BatchSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_EnrollmentTypes_TypeId",
                table: "Enrollments",
                column: "TypeId",
                principalTable: "EnrollmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
