using Microsoft.EntityFrameworkCore.Migrations;

namespace Sendo.Api.Migrations
{
    public partial class ExtendUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                schema: "user_data",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                schema: "user_data",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_hash",
                schema: "user_data",
                table: "user");

            migrationBuilder.DropColumn(
                name: "salt",
                schema: "user_data",
                table: "user");
        }
    }
}
