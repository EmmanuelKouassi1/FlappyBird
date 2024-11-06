using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlappyC_.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatlA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "Visibilité",
                table: "Score",
                newName: "Visibilite");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Score",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visibilite",
                table: "Score",
                newName: "Visibilité");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Score",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Score",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
