using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeApi.Migrations
{
    public partial class _030320222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "UserInfo",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Adresses");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UserInfo",
                newName: "email");
        }
    }
}
