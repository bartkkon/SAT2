using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class UpCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Year = table.Column<decimal>(nullable: false),
                    Euro = table.Column<decimal>(nullable: false),
                    Dolar = table.Column<decimal>(nullable: false),
                    Sek = table.Column<decimal>(nullable: false),
                    ECCC = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Year);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
