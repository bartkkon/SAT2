using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class PNCTotalityADD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PNC_Totality",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Structure = table.Column<string>(type: "varchar(5)", nullable: true),
                    Instalation = table.Column<string>(type: "varchar(4)", nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNC_Totality", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PNC_Totality");
        }
    }
}
