using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solver.APIClient.Migrations
{
    public partial class AddProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkDays",
                table: "WorkingDays",
                newName: "DayToWork");

            migrationBuilder.CreateTable(
                name: "ProgramConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    ManagmentProvider = table.Column<string>(nullable: true),
                    AnalyzeFileProvider = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    Identification = table.Column<string>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightLastElement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    ElementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightLastElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightLastElement_Elements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackLogDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    RowLine = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    TrackLogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackLogDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackLogDetails_TrackLogs_TrackLogId",
                        column: x => x.TrackLogId,
                        principalTable: "TrackLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackLogDetails_TrackLogId",
                table: "TrackLogDetails",
                column: "TrackLogId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightLastElement_ElementId",
                table: "WeightLastElement",
                column: "ElementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramConfigurations");

            migrationBuilder.DropTable(
                name: "TrackLogDetails");

            migrationBuilder.DropTable(
                name: "WeightLastElement");

            migrationBuilder.DropTable(
                name: "TrackLogs");

            migrationBuilder.RenameColumn(
                name: "DayToWork",
                table: "WorkingDays",
                newName: "WorkDays");
        }
    }
}
