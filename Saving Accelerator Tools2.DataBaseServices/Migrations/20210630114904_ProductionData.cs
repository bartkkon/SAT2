using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.DataBaseServices.Migrations
{
    public partial class ProductionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANCs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "varchar(9)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANCs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PNCPlatforms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Structure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Installation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNCPlatforms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PNCs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "varchar(9)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(14,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNCs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StandardCosts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "varchar(9)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDCO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STK3 = table.Column<decimal>(type: "decimal(12,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardCosts", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANCs");

            migrationBuilder.DropTable(
                name: "PNCPlatforms");

            migrationBuilder.DropTable(
                name: "PNCs");

            migrationBuilder.DropTable(
                name: "StandardCosts");
        }
    }
}
