using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleStore.Data.Migrations
{
    public partial class settingaddedabout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mission",
                table: "Setting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strategy",
                table: "Setting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vision",
                table: "Setting",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mission",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "Strategy",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "Vision",
                table: "Setting");
        }
    }
}
