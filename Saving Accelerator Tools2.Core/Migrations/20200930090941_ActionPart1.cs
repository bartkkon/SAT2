using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class ActionPart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Description = table.Column<string>(type: "varchar(1000)", nullable: true),
                    StartYear = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Month = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Action_Devision_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(nullable: false),
                    DevisionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_Devision_InterTable", x => new { x.ActionID, x.DevisionID });
                    table.ForeignKey(
                        name: "FK_Action_Devision_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_Devision_InterTable_Devision_DevisionID",
                        column: x => x.DevisionID,
                        principalTable: "Devision",
                        principalColumn: "DevisionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Action_Plant_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(nullable: false),
                    PlantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_Plant_InterTable", x => new { x.ActionID, x.PlantID });
                    table.ForeignKey(
                        name: "FK_Action_Plant_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_Plant_InterTable_Plant_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Plant",
                        principalColumn: "PlantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_Devision_InterTable_DevisionID",
                table: "Action_Devision_InterTable",
                column: "DevisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Action_Plant_InterTable_PlantID",
                table: "Action_Plant_InterTable",
                column: "PlantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action_Devision_InterTable");

            migrationBuilder.DropTable(
                name: "Action_Plant_InterTable");

            migrationBuilder.DropTable(
                name: "Action");
        }
    }
}
