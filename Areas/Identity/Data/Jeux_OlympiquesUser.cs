using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Jeux_Olympiques.Areas.Identity.Data;

/// <summary>
/// Création de la classe dérivée Jeux_OlympiqueUser qui hérite d'IdentityUser pour l'ajout de nouveaux champs
/// </summary>
public class Jeux_OlympiquesUser : IdentityUser
{
    /// <summary>
    /// Ajout du champ Prénom
    /// </summary>
    [PersonalData]
    public string? FirstName { get; set; }
    /// <summary>
    /// Ajout du champ Nom
    /// </summary>
    [PersonalData]
    public string? LastName { get; set; }
    /// <summary>
    /// Création du champ Clef unique
    /// </summary>
    [PersonalData]
    public string? AccountKey { get; private set; }
    /// <summary>
    /// Génère une clé unique (AccountKey) pour l'utilisateur en utilisant une chaîne combinant
    /// l'Id de l'utilisateur, son prénom (FirstName), son nom (LastName) et son Email.
    /// Cette combinaison est ensuite hachée avec l'algorithme SHA256 pour assurer l'unicité.
    /// </summary>
    /// /// <remarks>
    /// La clé générée est une chaîne hexadécimale en minuscules, dérivée du hachage SHA256.
    /// Attention, Microsoft actualise très souvent l'algorithme. 
    /// </remarks>
    public void GenerateAccountKey()
    {
        using var sha256 = SHA256.Create();
        var combinedValue = $"{Id}{FirstName}{LastName}{Email}";
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedValue));
        AccountKey = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}

