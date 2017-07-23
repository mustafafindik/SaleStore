using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleStore.Migrations
{
    public partial class MapFieldsAddedToCompanyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MapIsVisible",
                table: "Companies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MapLat",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapLon",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapTitle",
                table: "Companies",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisements",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Advertisements",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapIsVisible",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MapLat",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MapLon",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MapTitle",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisements",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Advertisements",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
