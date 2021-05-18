using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class AddResultsaddCreatedByinAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveAction",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Action",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBy",
                table: "Action",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "char(9)", nullable: true),
                    Revision = table.Column<string>(type: "char(3)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CarryOver = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    Savings = table.Column<decimal>(type: "decimal(16,4)", nullable: false),
                    ECCC = table.Column<decimal>(type: "decimal(16,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Action_Results_InterTable",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    ResultID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action_Results_InterTable", x => new { x.ActionID, x.ResultID });
                    table.ForeignKey(
                        name: "FK_Action_Results_InterTable_Action_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_Results_InterTable_Results_ResultID",
                        column: x => x.ResultID,
                        principalTable: "Results",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_Results_InterTable_ResultID",
                table: "Action_Results_InterTable",
                column: "ResultID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action_Results_InterTable");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropColumn(
                name: "ActiveAction",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "DateBy",
                table: "Action");
        }
    }
}
