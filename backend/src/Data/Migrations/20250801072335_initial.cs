using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    AssigneeId = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { "2316cedf-bb9c-4c33-9840-e1090071c9ca", new DateTime(2025, 8, 1, 7, 23, 33, 504, DateTimeKind.Utc).AddTicks(4523), "alok.kumar@smoothstack.com", "16B989E3420BE5F0EC9F1276DC3CE772", 1, "alok" },
                    { "8cc6b6cd-fdb0-4d4d-ad1b-92e0c043294a", new DateTime(2025, 8, 1, 7, 23, 33, 504, DateTimeKind.Utc).AddTicks(4490), "munetsilazzie@gmail.com", "87152AB405DB64027D236F03EE80D736", 1, "lazzie" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssigneeId", "CreatedAt", "CreatorId", "Description", "Priority", "Status", "Title", "UpdatedAt" },
                values: new object[] { "ae077477-53e3-4c94-967d-a31e086146e2", "8cc6b6cd-fdb0-4d4d-ad1b-92e0c043294a", new DateTime(2025, 8, 1, 7, 23, 33, 504, DateTimeKind.Utc).AddTicks(4668), "2316cedf-bb9c-4c33-9840-e1090071c9ca", "NOTE: Do not use AI tools to write the code. The code will be screened thoroughly. Use DotNet for Backend and React for Frontend", 2, 1, "React DotNet - Coding Assignment - Medlogix", new DateTime(2025, 8, 1, 7, 23, 33, 504, DateTimeKind.Utc).AddTicks(4669) });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
