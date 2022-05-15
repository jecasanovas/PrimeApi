using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeApi.Migrations
{
    public partial class addressmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeofDirection",
                table: "Adresses",
                newName: "TypeOfDirection");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "Adresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfDocument",
                table: "Adresses",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "TypeOfDocument",
                table: "Adresses");

            migrationBuilder.RenameColumn(
                name: "TypeOfDirection",
                table: "Adresses",
                newName: "TypeofDirection");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "Adresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
