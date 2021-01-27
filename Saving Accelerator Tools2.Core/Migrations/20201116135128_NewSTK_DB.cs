using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class NewSTK_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STK",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANC = table.Column<string>(type: "varchar(9)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDCO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<decimal>(type: "decimal(4,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STD = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STK", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STK");
        }
    }
}
