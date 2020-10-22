using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class PNC_ANC_Revision_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANC_Revision",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANC = table.Column<string>(type: "varchar(9)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANC_Revision", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PNC_Revision",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PNC = table.Column<string>(type: "varchar(9)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNC_Revision", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANC_Revision");

            migrationBuilder.DropTable(
                name: "PNC_Revision");
        }
    }
}
