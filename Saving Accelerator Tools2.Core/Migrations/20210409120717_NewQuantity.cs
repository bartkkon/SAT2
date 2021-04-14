using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class NewQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quantity_DB");

            migrationBuilder.CreateTable(
                name: "ANC_Quantity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "varchar(9)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANC_Quantity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PNC_Quantity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "varchar(9)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNC_Quantity", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANC_Quantity");

            migrationBuilder.DropTable(
                name: "PNC_Quantity");

            migrationBuilder.CreateTable(
                name: "Quantity_DB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "varchar(9)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantity_DB", x => x.ID);
                });
        }
    }
}
