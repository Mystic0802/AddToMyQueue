using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddToMyQueue.Data.Migrations
{
    public partial class AddToMyQueuev14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "SpotifyAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RecentAddedSongs",
                columns: table => new
                {
                    SpotifyId = table.Column<string>(type: "text", nullable: false),
                    SongUri = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecentAddedSongs", x => x.SpotifyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecentAddedSongs");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "SpotifyAccounts");
        }
    }
}
