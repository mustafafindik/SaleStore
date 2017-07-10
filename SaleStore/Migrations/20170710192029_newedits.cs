using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SaleStore.Migrations
{
    public partial class newedits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvertiseEndDate = table.Column<DateTime>(nullable: false),
                    AdvertiseStartDate = table.Column<DateTime>(nullable: false),
                    CampaignPage = table.Column<bool>(nullable: false),
                    ClickRate = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HTMLCodes = table.Column<string>(nullable: true),
                    HomePageCampaigns = table.Column<bool>(nullable: false),
                    HomePageProducts = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Ispublished = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PositionDegree = table.Column<int>(nullable: false),
                    ProductsPage = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    URLpath = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 30, nullable: true),
                    advType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");
        }
    }
}
