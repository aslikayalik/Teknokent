using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknokent.Migrations
{
    public partial class DeleteImgFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Statistic");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "CVFile",
                table: "Cv");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "CareerPosting");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "BoardOfMember");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Statistic",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Office",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "CVFile",
                table: "Cv",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "CareerPosting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "BoardOfMember",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
