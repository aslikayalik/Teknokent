using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknokent.Migrations
{
    public partial class UpdateNewsArticleBoardOfMemberStatisticModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "News");

            migrationBuilder.DropColumn(
                name: "IsBoardMember",
                table: "BoardOfMember");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Article");

            migrationBuilder.AlterColumn<string>(
                name: "ImgPath",
                table: "Statistic",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImgPath",
                table: "Statistic",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBoardMember",
                table: "BoardOfMember",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Article",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
