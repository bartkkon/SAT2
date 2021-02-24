using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class AdPNCCalcBYDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PNCList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PNC = table.Column<string>(type: "char(9)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNCList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Action_PNC_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    PNCID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_PNC_InterTable", x => new { x.ActionID, x.PNCID });
                    table.ForeignKey(
                        name: "FK_Action_PNC_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_PNC_InterTable_PNCList_PNCID",
                        column: x => x.PNCID,
                        principalTable: "PNCList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_PNC_InterTable_PNCID",
                table: "Action_PNC_InterTable",
                column: "PNCID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action_PNC_InterTable");

            migrationBuilder.DropTable(
                name: "PNCList");
        }
    }
}
