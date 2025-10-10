using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moodify.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSpotifyFieldsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpotifyDisplayName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpotifyEmail",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpotifyId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpotifyLinkedAt",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotifyDisplayName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpotifyEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpotifyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpotifyLinkedAt",
                table: "Users");
        }
    }
}
