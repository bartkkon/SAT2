using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class ApprovalsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Approvals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Revision = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Devision = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approvals");
        }
    }
}
