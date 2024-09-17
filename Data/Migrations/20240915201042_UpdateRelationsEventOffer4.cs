using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsEventOffer4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Events_EventId",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Offers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Events_EventId",
                table: "Offers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
