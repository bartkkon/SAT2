using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class Action_Plaform_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BI",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "D45",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DMD",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FI",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FS",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FSBU",
                table: "Action",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BI",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "D45",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "DMD",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "FI",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "FS",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "FSBU",
                table: "Action");
        }
    }
}
