using Microsoft.EntityFrameworkCore.Migrations;

namespace Solver.APIClient.Migrations
{
    public partial class AddPrograms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowLine",
                table: "TrackLogDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowLine",
                table: "TrackLogDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}
