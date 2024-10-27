using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknokent.Migrations
{
    public partial class UpdateRefereeRegistrationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TC",
                table: "RefereeRegistration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TC",
                table: "RefereeRegistration");
        }
    }
}
