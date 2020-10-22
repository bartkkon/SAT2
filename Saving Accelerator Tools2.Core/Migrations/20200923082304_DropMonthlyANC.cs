using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class DropMonthlyANC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANC_Monthly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANC_Monthly",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    April = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    August = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    December = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    February = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    January = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    July = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    June = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    March = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    May = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    November = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    October = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    September = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANC_Monthly", x => x.ID);
                });
        }
    }
}
