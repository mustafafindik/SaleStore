using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleStore.Migrations
{
    public partial class CampaignModelEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CompanyId",
                table: "Campaigns",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Companies_CompanyId",
                table: "Campaigns",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Companies_CompanyId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_CompanyId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Campaigns");
        }
    }
}
