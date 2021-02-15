using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class ANCChange_AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANCChange",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldANC = table.Column<string>(type: "varchar(9)", nullable: true),
                    OldANCQuantity = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    OldSTK = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    NewANC = table.Column<string>(type: "varchar(9)", nullable: true),
                    NewANCQuantity = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    NewSTK = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Delta = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    UserEstymacja = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Estymacja = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    NextANC1 = table.Column<string>(type: "varchar(9)", nullable: true),
                    NextANC2 = table.Column<string>(type: "varchar(9)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANCChange", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Action_ANCChage_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    ANCChangeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_ANCChage_InterTable", x => new { x.ActionID, x.ANCChangeID });
                    table.ForeignKey(
                        name: "FK_Action_ANCChage_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_ANCChage_InterTable_ANCChange_ANCChangeID",
                        column: x => x.ANCChangeID,
                        principalTable: "ANCChange",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_ANCChage_InterTable_ANCChangeID",
                table: "Action_ANCChage_InterTable",
                column: "ANCChangeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action_ANCChage_InterTable");

            migrationBuilder.DropTable(
                name: "ANCChange");
        }
    }
}
