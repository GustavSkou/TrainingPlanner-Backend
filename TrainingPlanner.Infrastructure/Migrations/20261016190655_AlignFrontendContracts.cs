using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlignFrontendContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Pace",
                table: "Workouts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaceUnit",
                table: "Workouts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pace",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "PaceUnit",
                table: "Workouts");

        }
    }
}
