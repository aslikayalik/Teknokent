using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknokent.Migrations
{
    public partial class UpdateNewsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "News");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "News",
                type: "datetime2",
                nullable: true);
        }
    }
}
