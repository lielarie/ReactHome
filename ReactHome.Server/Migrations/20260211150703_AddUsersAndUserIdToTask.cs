using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactHome.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndUserIdToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDetails_Email",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserDetails_FullName",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserDetails_Phone",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "UserDetails_Id",
                table: "Tasks",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_User_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_User_UserId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tasks",
                newName: "UserDetails_Id");

            migrationBuilder.AddColumn<string>(
                name: "UserDetails_Email",
                table: "Tasks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserDetails_FullName",
                table: "Tasks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserDetails_Phone",
                table: "Tasks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
