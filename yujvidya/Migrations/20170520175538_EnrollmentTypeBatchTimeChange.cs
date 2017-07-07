using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace yujvidya.Migrations
{
    public partial class EnrollmentTypeBatchTimeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchType",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PreferredBatch",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "PreferredBatchId",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Enrollments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BatchSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Days = table.Column<int>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Amount = table.Column<double>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentTypes", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_BatchSchedules_PreferredBatchId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_EnrollmentTypes_TypeId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "BatchSchedules");

            migrationBuilder.DropTable(
                name: "EnrollmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_PreferredBatchId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_TypeId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "PreferredBatchId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "BatchType",
                table: "Persons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferredBatch",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);
        }
    }
}
