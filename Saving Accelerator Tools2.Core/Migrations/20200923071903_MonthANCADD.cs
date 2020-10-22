using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class MonthANCADD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANC_Monthly",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANC = table.Column<string>(nullable: true),
                    Year = table.Column<decimal>(nullable: false),
                    January = table.Column<decimal>(nullable: false),
                    February = table.Column<decimal>(nullable: false),
                    March = table.Column<decimal>(nullable: false),
                    April = table.Column<decimal>(nullable: false),
                    May = table.Column<decimal>(nullable: false),
                    June = table.Column<decimal>(nullable: false),
                    July = table.Column<decimal>(nullable: false),
                    August = table.Column<decimal>(nullable: false),
                    September = table.Column<decimal>(nullable: false),
                    October = table.Column<decimal>(nullable: false),
                    November = table.Column<decimal>(nullable: false),
                    December = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANC_Monthly", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANC_Monthly");
        }
    }
}
