using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddToMyQueue.Data.Migrations
{
    public partial class AddToMyQueuev12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpotifyUsers");

            migrationBuilder.CreateTable(
                name: "SpotifyAccounts",
                columns: table => new
                {
                    SpotifyId = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    TokenCreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpotifyAccounts", x => x.SpotifyId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserSpotifyAccounts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SpotifyId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSpotifyAccounts", x => new { x.UserId, x.SpotifyId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpotifyAccounts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserSpotifyAccounts");

            migrationBuilder.CreateTable(
                name: "SpotifyUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    TokenCreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserGuid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpotifyUsers", x => x.UserId);
                });
        }
    }
}
