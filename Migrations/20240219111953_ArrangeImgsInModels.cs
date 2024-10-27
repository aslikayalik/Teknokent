using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknokent.Migrations
{
    public partial class ArrangeImgsInModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Statistic",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Office",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "CareerPosting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "BoardOfMember",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Statistic");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "CareerPosting");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "BoardOfMember");
        }
    }
}
