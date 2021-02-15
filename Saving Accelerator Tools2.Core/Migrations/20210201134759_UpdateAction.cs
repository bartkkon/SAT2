using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class UpdateAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "STD",
                table: "STK",
                type: "decimal(12,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,4)");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Action_Leader_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    LeaderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_Leader_InterTable", x => new { x.ActionID, x.LeaderID });
                    table.ForeignKey(
                        name: "FK_Action_Leader_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_Leader_InterTable_ActionLeader_LeaderID",
                        column: x => x.LeaderID,
                        principalTable: "ActionLeader",
                        principalColumn: "LeaderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Action_Tag_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_Tag_InterTable", x => new { x.ActionID, x.TagID });
                    table.ForeignKey(
                        name: "FK_Action_Tag_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_Tag_InterTable_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_Leader_InterTable_LeaderID",
                table: "Action_Leader_InterTable",
                column: "LeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Action_Tag_InterTable_TagID",
                table: "Action_Tag_InterTable",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action_Leader_InterTable");

            migrationBuilder.DropTable(
                name: "Action_Tag_InterTable");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Action");

            migrationBuilder.AlterColumn<decimal>(
                name: "STD",
                table: "STK",
                type: "decimal(8,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,4)");
        }
    }
}
