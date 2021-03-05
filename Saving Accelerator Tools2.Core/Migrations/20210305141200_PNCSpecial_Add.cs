using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class PNCSpecial_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PNCSpecial_ANC",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Old_ANC = table.Column<string>(type: "char(9)", nullable: true),
                    Old_Q = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    New_ANC = table.Column<string>(type: "char(9)", nullable: true),
                    New_Q = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    Old_STK = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    New_STK = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Delta = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNCSpecial_ANC", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PNCSpecial_PNC",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PNC = table.Column<string>(type: "char(9)", nullable: true),
                    ECCC = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    Old_STK = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    New_STK = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Delta = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNCSpecial_PNC", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Action_PNCSpecial_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    PNCSpecID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_PNCSpecial_InterTable", x => new { x.ActionID, x.PNCSpecID });
                    table.ForeignKey(
                        name: "FK_Action_PNCSpecial_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_PNCSpecial_InterTable_PNCSpecial_PNC_PNCSpecID",
                        column: x => x.PNCSpecID,
                        principalTable: "PNCSpecial_PNC",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PNCSPecial_PNC_ANC_InterTable",
                columns: table => new
                {
                    PNC_ID = table.Column<int>(type: "int", nullable: false),
                    ANC_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNCSPecial_PNC_ANC_InterTable", x => new { x.PNC_ID, x.ANC_ID });
                    table.ForeignKey(
                        name: "FK_PNCSPecial_PNC_ANC_InterTable_PNCSpecial_ANC_ANC_ID",
                        column: x => x.ANC_ID,
                        principalTable: "PNCSpecial_ANC",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PNCSPecial_PNC_ANC_InterTable_PNCSpecial_PNC_PNC_ID",
                        column: x => x.PNC_ID,
                        principalTable: "PNCSpecial_PNC",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_PNCSpecial_InterTable_PNCSpecID",
                table: "Action_PNCSpecial_InterTable",
                column: "PNCSpecID");

            migrationBuilder.CreateIndex(
                name: "IX_PNCSPecial_PNC_ANC_InterTable_ANC_ID",
                table: "PNCSPecial_PNC_ANC_InterTable",
                column: "ANC_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action_PNCSpecial_InterTable");

            migrationBuilder.DropTable(
                name: "PNCSPecial_PNC_ANC_InterTable");

            migrationBuilder.DropTable(
                name: "PNCSpecial_ANC");

            migrationBuilder.DropTable(
                name: "PNCSpecial_PNC");
        }
    }
}
