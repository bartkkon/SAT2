using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class AddSTK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STK",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANC = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IDCO = table.Column<string>(nullable: true),
                    Year = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    STD = table.Column<decimal>(nullable: false)
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
