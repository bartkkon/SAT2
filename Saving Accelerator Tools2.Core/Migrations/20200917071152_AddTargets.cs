using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class AddTargets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    Year = table.Column<decimal>(nullable: false),
                    Revision = table.Column<string>(nullable: true),
                    DM = table.Column<decimal>(nullable: false),
                    Electronic = table.Column<decimal>(nullable: false),
                    Mechanic = table.Column<decimal>(nullable: false),
                    NVR = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.Year);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Targets");
        }
    }
}
