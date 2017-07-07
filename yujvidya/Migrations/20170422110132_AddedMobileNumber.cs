using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace yujvidya.Migrations
{
    public partial class AddedMobileNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("MobileNumber", "Persons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
