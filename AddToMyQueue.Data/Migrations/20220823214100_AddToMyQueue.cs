using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddToMyQueue.Data.Migrations
{
    public partial class AddToMyQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreationTime",
                table: "SpotifyUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenCreationTime",
                table: "SpotifyUsers");
        }
    }
}
