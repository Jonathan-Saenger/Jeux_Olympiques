using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jeux_Olympiques.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountKey", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c7a42f97-4108-4ff9-bdee-326866513a03", 0, null, "3967ca6c-926b-4610-93f8-e4ee58de29ef", "Jeux_OlympiquesUser", "user@jeuxolympiques.com", true, "Franck", "Letesteur", true, null, "USER@JEUXOLYMPIQUES.COM", "USER@JEUXOLYMPIQUES.COM", "AQAAAAIAAYagAAAAEAuQECgPLnpmQ3uGf3DY87bFAf1ny8toJ0OsyDbLnnruDv3QdxbYUe273bLsX39Eeg==", "NULL", false, "LZBL3DTGYNXJPNQ3ZJ2JN3ZNI54WA4T3", false, "user@jeuxolympiques.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7a42f97-4108-4ff9-bdee-326866513a03");
        }
    }
}
