using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCartAndTicketDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Offers_OfferId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Offers_ContainsId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ContainsId",
                table: "Tickets",
                newName: "ContainsOfferId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tickets",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ContainsId",
                table: "Tickets",
                newName: "IX_Tickets_ContainsOfferId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Offers",
                newName: "OfferId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carts",
                newName: "RecordId");

            migrationBuilder.AddColumn<DateTime>(
                name: "TicketDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    TicketDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetails", x => x.TicketDetailId);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_OfferId",
                table: "TicketDetails",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Offers_OfferId",
                table: "Carts",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Offers_ContainsOfferId",
                table: "Tickets",
                column: "ContainsOfferId",
                principalTable: "Offers",
                principalColumn: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Offers_OfferId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Offers_ContainsOfferId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropColumn(
                name: "TicketDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "ContainsOfferId",
                table: "Tickets",
                newName: "ContainsId");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Tickets",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ContainsOfferId",
                table: "Tickets",
                newName: "IX_Tickets_ContainsId");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Offers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Carts",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Offers_OfferId",
                table: "Carts",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Offers_ContainsId",
                table: "Tickets",
                column: "ContainsId",
                principalTable: "Offers",
                principalColumn: "Id");
        }
    }
}
