using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.DataBaseServices.Migrations
{
    public partial class Dataupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MailSubscription",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactoryID",
                table: "StandardCosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactoryID",
                table: "PNCs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactoryID",
                table: "PNCPlatforms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactoryID",
                table: "ANCs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StandardCosts_FactoryID",
                table: "StandardCosts",
                column: "FactoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PNCs_FactoryID",
                table: "PNCs",
                column: "FactoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PNCPlatforms_FactoryID",
                table: "PNCPlatforms",
                column: "FactoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ANCs_FactoryID",
                table: "ANCs",
                column: "FactoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_ANCs_Factories_FactoryID",
                table: "ANCs",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PNCPlatforms_Factories_FactoryID",
                table: "PNCPlatforms",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PNCs_Factories_FactoryID",
                table: "PNCs",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StandardCosts_Factories_FactoryID",
                table: "StandardCosts",
                column: "FactoryID",
                principalTable: "Factories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ANCs_Factories_FactoryID",
                table: "ANCs");

            migrationBuilder.DropForeignKey(
                name: "FK_PNCPlatforms_Factories_FactoryID",
                table: "PNCPlatforms");

            migrationBuilder.DropForeignKey(
                name: "FK_PNCs_Factories_FactoryID",
                table: "PNCs");

            migrationBuilder.DropForeignKey(
                name: "FK_StandardCosts_Factories_FactoryID",
                table: "StandardCosts");

            migrationBuilder.DropIndex(
                name: "IX_StandardCosts_FactoryID",
                table: "StandardCosts");

            migrationBuilder.DropIndex(
                name: "IX_PNCs_FactoryID",
                table: "PNCs");

            migrationBuilder.DropIndex(
                name: "IX_PNCPlatforms_FactoryID",
                table: "PNCPlatforms");

            migrationBuilder.DropIndex(
                name: "IX_ANCs_FactoryID",
                table: "ANCs");

            migrationBuilder.DropColumn(
                name: "MailSubscription",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FactoryID",
                table: "StandardCosts");

            migrationBuilder.DropColumn(
                name: "FactoryID",
                table: "PNCs");

            migrationBuilder.DropColumn(
                name: "FactoryID",
                table: "PNCPlatforms");

            migrationBuilder.DropColumn(
                name: "FactoryID",
                table: "ANCs");
        }
    }
}
