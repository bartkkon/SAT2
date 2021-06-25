using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class AddNewApprovalsTable_RemoveOldQuntiyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANC_Monthly");

            migrationBuilder.DropTable(
                name: "ANC_Revision");

            migrationBuilder.DropTable(
                name: "PNC_Monthly");

            migrationBuilder.DropTable(
                name: "PNC_Revision");

            migrationBuilder.CreateTable(
                name: "Approvals2",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Plant = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals2", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Approvals2_Dev",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Devision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals2_Dev", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Approvals_IT",
                columns: table => new
                {
                    Approvals_ID = table.Column<int>(type: "int", nullable: false),
                    Devision_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals_IT", x => new { x.Approvals_ID, x.Devision_ID });
                    table.ForeignKey(
                        name: "FK_Approvals_IT_Approvals2_Approvals_ID",
                        column: x => x.Approvals_ID,
                        principalTable: "Approvals2",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvals_IT_Approvals2_Dev_Devision_ID",
                        column: x => x.Devision_ID,
                        principalTable: "Approvals2_Dev",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_IT_Devision_ID",
                table: "Approvals_IT",
                column: "Devision_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approvals_IT");

            migrationBuilder.DropTable(
                name: "Approvals2");

            migrationBuilder.DropTable(
                name: "Approvals2_Dev");

            migrationBuilder.CreateTable(
                name: "ANC_Monthly",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANC = table.Column<string>(type: "varchar(9)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANC_Monthly", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ANC_Revision",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANC = table.Column<string>(type: "varchar(9)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANC_Revision", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PNC_Monthly",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    PNC = table.Column<string>(type: "varchar(9)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNC_Monthly", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PNC_Revision",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    PNC = table.Column<string>(type: "varchar(9)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Revision = table.Column<string>(type: "varchar(3)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PNC_Revision", x => x.ID);
                });
        }
    }
}
