using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1DBFDCBB-4D11-4562-9756-602889FD4163");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59C78292-150C-4218-B08A-ACED1F79A45B", "F92315DD - 48DD - 4EA1 - A4C3 - C532D81843B6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountKey", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1DBFDCBB-4D11-4562-9756-602889FD4163", 0, null, "05e593f3-cb5b-4597-a926-e9bbf804289f", "Jeux_OlympiquesUser", "admin@jeuxolympiques.com", true, "AdminPrenom", "AdminNom", true, null, "ADMIN@JEUXOLYMPIQUES.COM", "ADMIN@JEUXOLYMPIQUES.COM", "AQAAAAIAAYagAAAAEG1swvJSZyrab4nmV8Dl1AaNZIz/Owlf9869lZ7X0Vxr3DCd/XzSutmpTNTMddrNjA==", "NULL", false, "65K4MQIYK4OSUVYHUY46KRXZ3EUBNO6R", false, "admin@jeuxolympiques.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "59C78292-150C-4218-B08A-ACED1F79A45B", "1DBFDCBB-4D11-4562-9756-602889FD4163" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "59C78292-150C-4218-B08A-ACED1F79A45B", "1DBFDCBB-4D11-4562-9756-602889FD4163" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59C78292-150C-4218-B08A-ACED1F79A45B");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1DBFDCBB-4D11-4562-9756-602889FD4163");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountKey", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1DBFDCBB-4D11-4562-9756-602889FD4163", 0, null, "05e593f3-cb5b-4597-a926-e9bbf804289f", "User", "admin@jeuxolympiques.com", true, "AdminPrenom", "AdminNom", true, null, "null", "ADMIN@JEUXOLYMPIQUES.COM", "ADMIN@JEUXOLYMPIQUES.COM", "AQAAAAIAAYagAAAAEG1swvJSZyrab4nmV8Dl1AaNZIz/Owlf9869lZ7X0Vxr3DCd/XzSutmpTNTMddrNjA==", "NULL", false, "65K4MQIYK4OSUVYHUY46KRXZ3EUBNO6R", false, "admin@jeuxolympiques.com" });
        }
    }
}
