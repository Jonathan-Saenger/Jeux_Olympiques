using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsEventOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Offers_OfferId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_OfferId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Events");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_EventId",
                table: "Offers",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Events_EventId",
                table: "Offers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Events_EventId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_EventId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Offers");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Tickets",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_OfferId",
                table: "Events",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Offers_OfferId",
                table: "Events",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }
    }
}
