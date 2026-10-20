using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserNamesToUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Username");

            migrationBuilder.Sql(
                "UPDATE [Users] SET [Username] = LTRIM(RTRIM(CONCAT([Username], ' ', [LastName]))) WHERE [LastName] <> ''");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
