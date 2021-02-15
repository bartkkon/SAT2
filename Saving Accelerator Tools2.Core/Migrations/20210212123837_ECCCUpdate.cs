using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class ECCCUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ECCC",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ECCCSpec",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ECCCValue",
                table: "Action",
                type: "decimal(5,1)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ECCC",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "ECCCSpec",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "ECCCValue",
                table: "Action");
        }
    }
}
