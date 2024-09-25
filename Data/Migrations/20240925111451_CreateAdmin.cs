using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountKey", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1DBFDCBB-4D11-4562-9756-602889FD4163", 0, null, "05e593f3-cb5b-4597-a926-e9bbf804289f", "User", "admin@jeuxolympiques.com", true, "AdminPrenom", "AdminNom", true, null, "null", "ADMIN@JEUXOLYMPIQUES.COM", "ADMIN@JEUXOLYMPIQUES.COM", "AQAAAAIAAYagAAAAEG1swvJSZyrab4nmV8Dl1AaNZIz/Owlf9869lZ7X0Vxr3DCd/XzSutmpTNTMddrNjA==", "NULL", false, "65K4MQIYK4OSUVYHUY46KRXZ3EUBNO6R", false, "admin@jeuxolympiques.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1DBFDCBB-4D11-4562-9756-602889FD4163");
        }
    }
}
