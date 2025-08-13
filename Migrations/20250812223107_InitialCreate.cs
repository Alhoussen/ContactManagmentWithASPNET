using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "FirstName", "LastName", "PhoneNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "123 Rue de la Paix, Paris", new DateTime(2025, 7, 13, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5486), "jean.dupont@email.com", "Jean", "Dupont", "0123456789", new DateTime(2025, 7, 13, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5757) },
                    { 2, "456 Avenue des Champs, Lyon", new DateTime(2025, 7, 23, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5919), "marie.martin@email.com", "Marie", "Martin", "0987654321", new DateTime(2025, 7, 23, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5921) },
                    { 3, null, new DateTime(2025, 8, 2, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5924), "pierre.durand@email.com", "Pierre", "Durand", "0147258369", new DateTime(2025, 8, 2, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5925) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CreatedAt",
                table: "Contacts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Email",
                table: "Contacts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Name",
                table: "Contacts",
                columns: new[] { "FirstName", "LastName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
