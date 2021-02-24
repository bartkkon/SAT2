using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class ANCChange_Special : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action_ANCChangePlatform_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    ChangeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_ANCChangePlatform_InterTable", x => new { x.ActionID, x.ChangeID });
                    table.ForeignKey(
                        name: "FK_Action_ANCChangePlatform_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_ANCChangePlatform_InterTable_PlatformCalcs_ChangeID",
                        column: x => x.ChangeID,
                        principalTable: "PlatformCalcs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ANCSpecial_Item",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "char(9)", nullable: true),
                    Plus = table.Column<bool>(type: "bit", nullable: false),
                    Minus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANCSpecial_Item", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Action_ANCChange_Items_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_ANCChange_Items_InterTable", x => new { x.ActionID, x.ItemID });
                    table.ForeignKey(
                        name: "FK_Action_ANCChange_Items_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_ANCChange_Items_InterTable_ANCSpecial_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "ANCSpecial_Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_ANCChange_Items_InterTable_ItemID",
                table: "Action_ANCChange_Items_InterTable",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Action_ANCChangePlatform_InterTable_ChangeID",
                table: "Action_ANCChangePlatform_InterTable",
                column: "ChangeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action_ANCChange_Items_InterTable");

            migrationBuilder.DropTable(
                name: "Action_ANCChangePlatform_InterTable");

            migrationBuilder.DropTable(
                name: "ANCSpecial_Item");
        }
    }
}
