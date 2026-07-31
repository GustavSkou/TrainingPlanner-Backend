using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrainingPlanremovedatarange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TrainingPlans");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "TrainingPlans",
                newName: "Date");

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 7, 30, 22, 0, 0, 0, DateTimeKind.Utc), "", "Running" });

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 7, 30, 22, 0, 0, 0, DateTimeKind.Utc), "", "Cycling" });

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 7, 30, 22, 0, 0, 0, DateTimeKind.Utc), "", "Swimming" });

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 7, 30, 22, 0, 0, 0, DateTimeKind.Utc), "", "Workout" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TrainingPlans",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TrainingPlans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TrainingPlans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Strength-focused training plans", "Strength" });

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Endurance and stamina-focused training plans", "Endurance" });

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mobility and flexibility-focused training plans", "Mobility" });

            migrationBuilder.UpdateData(
                table: "TrainingTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "Name" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Balanced general fitness training plans", "General Fitness" });
        }
    }
}
