using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeApi.Migrations
{
    public partial class teacherdesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Teachers");
        }
    }
}
