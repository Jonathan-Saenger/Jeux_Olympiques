using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Jeux_Olympiques.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Jeux_OlympiquesUser class
public class Jeux_OlympiquesUser : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }

    [PersonalData]
    public string? LastName { get; set; }

    [PersonalData]
    public string? AccountKey { get; private set; }

    // Méthode pour générer la clé unique
    public void GenerateAccountKey()
    {
        using (var sha256 = SHA256.Create())
        {
            var combinedValue = $"{Id}{FirstName}{LastName}{Email}";
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedValue));
            AccountKey = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

