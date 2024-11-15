using Jeux_Olympiques.Areas.Identity.Data;
using System.Diagnostics;

namespace Jeux_OlympiquesTests.UserAccountKeyTest
{
    /// <summary>
    /// Classe de test pour la fonctionnalité de génération d'une clé unique pour un User 
    /// </summary>
    [TestClass()]
    public class Jeux_OlympiquesUserTests
    {
        /// <summary>
        /// Méthode de test pour la génération de la clé
        /// </summary>
        [TestMethod()]
        public void GenerateAccountKey()
        {
            // Arrange: création de deux utilisateurs avec des informations d'identité
            var user1 = new Jeux_OlympiquesUser { Id = "1", Email = "john.doe@testmail.com", FirstName = "John", LastName = "Doe" };
            var user2 = new Jeux_OlympiquesUser { Id = "2", Email = "jane.deo@mailtest.com", FirstName = "Jane", LastName = "Deo" };

            // Act
            user1.GenerateAccountKey();
            user2.GenerateAccountKey();

            // Assert: on test la génération des clés et on vérifie l'unicité de chaque clé
            Assert.AreNotEqual(user1.AccountKey, user2.AccountKey);
            Assert.IsNotNull(user1.AccountKey);
            Assert.IsNotNull(user2.AccountKey);

            Debug.WriteLine($"User1 AccountKey: {user1.AccountKey}");
            Debug.WriteLine($"User2 AccountKey: {user2.AccountKey}");
        }
    }
}