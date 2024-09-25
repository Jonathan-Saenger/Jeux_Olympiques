using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewEventsOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedId", "Date", "Entitled", "Presentation", "Site" },
                values: new object[,]
                {
                    { 4, null, new DateTime(2024, 9, 11, 10, 15, 0, 0, DateTimeKind.Unspecified), "Tir à l'arc", "8ème de finale du Tir à l'Arc homme", "Pelouse du Stade de Vincenne" },
                    { 5, null, new DateTime(2024, 9, 12, 12, 30, 0, 0, DateTimeKind.Unspecified), "Natation", "Quart de finale de Natation Femme 500 mètres", "Piscine du Parc des Princes" },
                    { 6, null, new DateTime(2024, 9, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), "Ping Pong", "Finale Homme France - Chine", "Salle du Gymnase du Luxembourg" }
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
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
