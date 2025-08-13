using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 10, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 20, 12, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 13, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5486), new DateTime(2025, 7, 13, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5757) });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 23, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5919), new DateTime(2025, 7, 23, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5921) });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 2, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5924), new DateTime(2025, 8, 2, 22, 31, 7, 337, DateTimeKind.Utc).AddTicks(5925) });
        }
    }
}
