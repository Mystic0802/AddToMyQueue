﻿// <auto-generated />
using System;
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
    [Migration("20220823214100_AddToMyQueue")]
    partial class AddToMyQueue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("AddToMyQueue.Data.SpotifyUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("UserId"));

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TokenCreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserGuid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("SpotifyUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
