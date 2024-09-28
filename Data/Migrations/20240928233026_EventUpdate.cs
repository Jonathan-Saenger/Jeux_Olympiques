using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedId", "Date", "Entitled", "Presentation", "Site" },
                values: new object[,]
                {
                    { 4, null, new DateTime(2024, 9, 11, 10, 15, 0, 0, DateTimeKind.Unspecified), 3, "8ème de finale Escrime Homme", 3 },
                    { 5, null, new DateTime(2024, 9, 12, 12, 30, 0, 0, DateTimeKind.Unspecified), 2, "Quart de finale de Natation Femme 500 mètres", 4 },
                    { 6, null, new DateTime(2024, 9, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), 7, "Finale Homme France - Chine", 3 }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "Description", "EventId", "Place", "Price", "PublishId", "Title" },
                values: new object[,]
                {
                    { 14, "Entrée pour 1 personne", 4, "Placement libre", 29m, null, "OFFRE SOLO" },
                    { 15, "Entrée pour 2 personnes", 4, "Placement libre", 55m, null, "OFFRE DUO" },
                    { 16, "Entrée pour 4 personnes", 4, "Placement libre", 100m, null, "OFFRE FAMILLE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
