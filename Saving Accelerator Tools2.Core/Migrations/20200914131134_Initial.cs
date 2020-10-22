using Microsoft.EntityFrameworkCore.Migrations;

namespace Saving_Accelerator_Tools2.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionLeader",
                columns: table => new
                {
                    LeaderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLeader", x => x.LeaderID);
                });

            migrationBuilder.CreateTable(
                name: "Devision",
                columns: table => new
                {
                    DevisionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Devision = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devision", x => x.DevisionID);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tab = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    PlantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plant = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.PlantID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ActionLeader_Devision_DB",
                columns: table => new
                {
                    LeaderID = table.Column<int>(nullable: false),
                    DevisionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLeader_Devision_DB", x => new { x.LeaderID, x.DevisionID });
                    table.ForeignKey(
                        name: "FK_ActionLeader_Devision_DB_Devision_DevisionID",
                        column: x => x.DevisionID,
                        principalTable: "Devision",
                        principalColumn: "DevisionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionLeader_Devision_DB_ActionLeader_LeaderID",
                        column: x => x.LeaderID,
                        principalTable: "ActionLeader",
                        principalColumn: "LeaderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionLeader_Plant_DB",
                columns: table => new
                {
                    LeaderID = table.Column<int>(nullable: false),
                    FactoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLeader_Plant_DB", x => new { x.LeaderID, x.FactoryID });
                    table.ForeignKey(
                        name: "FK_ActionLeader_Plant_DB_Plant_FactoryID",
                        column: x => x.FactoryID,
                        principalTable: "Plant",
                        principalColumn: "PlantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionLeader_Plant_DB_ActionLeader_LeaderID",
                        column: x => x.LeaderID,
                        principalTable: "ActionLeader",
                        principalColumn: "LeaderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Devision_DB",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    DevisionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Devision_DB", x => new { x.UserID, x.DevisionID });
                    table.ForeignKey(
                        name: "FK_User_Devision_DB_Devision_DevisionID",
                        column: x => x.DevisionID,
                        principalTable: "Devision",
                        principalColumn: "DevisionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Devision_DB_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Pages_DB",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    PageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Pages_DB", x => new { x.UserID, x.PageID });
                    table.ForeignKey(
                        name: "FK_User_Pages_DB_Page_PageID",
                        column: x => x.PageID,
                        principalTable: "Page",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Pages_DB_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Role_DB",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Role_DB", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_User_Role_DB_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_DB_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPlant_DB",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    PlantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlant_DB", x => new { x.UserID, x.PlantID });
                    table.ForeignKey(
                        name: "FK_UserPlant_DB_Plant_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Plant",
                        principalColumn: "PlantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlant_DB_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLeader_Devision_DB_DevisionID",
                table: "ActionLeader_Devision_DB",
                column: "DevisionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLeader_Plant_DB_FactoryID",
                table: "ActionLeader_Plant_DB",
                column: "FactoryID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Devision_DB_DevisionID",
                table: "User_Devision_DB",
                column: "DevisionID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Pages_DB_PageID",
                table: "User_Pages_DB",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_DB_RoleID",
                table: "User_Role_DB",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlant_DB_PlantID",
                table: "UserPlant_DB",
                column: "PlantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLeader_Devision_DB");

            migrationBuilder.DropTable(
                name: "ActionLeader_Plant_DB");

            migrationBuilder.DropTable(
                name: "User_Devision_DB");

            migrationBuilder.DropTable(
                name: "User_Pages_DB");

            migrationBuilder.DropTable(
                name: "User_Role_DB");

            migrationBuilder.DropTable(
                name: "UserPlant_DB");

            migrationBuilder.DropTable(
                name: "ActionLeader");

            migrationBuilder.DropTable(
                name: "Devision");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
