// <auto-generated />
using AddToMyQueue.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AddToMyQueue.Data.Migrations
{
    [DbContext(typeof(AddToMyQueueContext))]
    [Migration("20220904175526_v1.0")]
    partial class v10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("AddToMyQueue.Data.Models.Spotify.AddedSong", b =>
                {
                    b.Property<string>("SpotifyId")
                        .HasColumnType("text");

                    b.Property<string>("SongUri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SpotifyId");

                    b.ToTable("RecentAddedSongs");
                });

            modelBuilder.Entity("AddToMyQueue.Data.Models.Spotify.SpotifyAccount", b =>
                {
                    b.Property<string>("SpotifyId")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SpotifyUsername")
                        .HasColumnType("text");

                    b.HasKey("SpotifyId");

                    b.ToTable("SpotifyAccounts");
                });

            modelBuilder.Entity("AddToMyQueue.Data.Models.Spotify.UserSpotifyAccount", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("SpotifyId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "SpotifyId");

                    b.ToTable("UserSpotifyAccounts");
                });

            modelBuilder.Entity("AddToMyQueue.Data.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
