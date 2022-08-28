using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddToMyQueue.Data.Migrations
{
    public partial class AddToMyQueuev13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenCreationTime",
                table: "SpotifyAccounts");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "SpotifyAccounts",
                newName: "SpotifyUsername");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpotifyUsername",
                table: "SpotifyAccounts",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreationTime",
                table: "SpotifyAccounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
