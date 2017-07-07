using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace yujvidya.Migrations
{
    public partial class AddedDurationColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "EnrollmentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationType",
                table: "EnrollmentTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "EnrollmentTypes");

            migrationBuilder.DropColumn(
                name: "DurationType",
                table: "EnrollmentTypes");
        }
    }
}
