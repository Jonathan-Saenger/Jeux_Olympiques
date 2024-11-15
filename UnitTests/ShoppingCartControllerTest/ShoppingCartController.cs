using System;

namespace Jeux_OlympiquesTests.ShoppingCartControllerTest
{
    /// <summary>
    /// Classe de test pour les fonctionnalités du panier d'achats
    /// </summary>
    [TestClass()]
    public class ShoppingCartTests
    {
        public ShoppingCartTests() { }

        /// <summary>
        /// Méthode de test pour l'ajout d'une offre au panier
        /// </summary>
        [TestMethod()]
        public async Task AddToCartTest()
        {
            // Arrange: Création d'une liste d'offres avec une offre
            var offers = new List<Offer>
            {
                new() { OfferId = 1, Title = "Offre 1", Price = 100m }
            };

            var mockContext = new MockDbContext(offers);
            var mockCart = new MockCart();

            // Simule l'ajout d'une offre au panier
            var addedOffer = await mockContext.SingleAsync(o => o.OfferId == 1);
            mockCart.AddToCart(addedOffer);

            // Assert: on teste que le nombre d'offres ajoutées au panier est bien de 1 et que l'ID de l'offre ajoutée est correct
            Assert.AreEqual(1, mockCart.AddedOffers.Count);
            Assert.AreEqual(1, mockCart.AddedOffers[0].OfferId);

            Console.WriteLine($"Test réussi: Offre ajoutée au panier - {mockCart.AddedOffers[0].Title} au prix de {mockCart.AddedOffers[0].Price} EUR");
        }
    }

    /// <summary>
    /// Classes de simulation de la base de données
    /// </summary>
    public class MockDbContext
    {
        public List<Offer> Offers { get; set; }

        public MockDbContext(List<Offer> offers)
        {
            Offers = offers;
        }

        public async Task<Offer> SingleAsync(System.Linq.Expressions.Expression<Func<Offer, bool>> predicate)
        {
            return await Task.FromResult(Offers.SingleOrDefault(predicate.Compile()));
        }
    }

    /// <summary>
    /// Classe simulée pour le panier d'achats
    /// </summary>
    public class MockCart : Jeux_Olympiques.Models.Cart
    {
        public List<Offer> AddedOffers { get; } = [];
        public void AddToCart(Offer offer)
        {
            AddedOffers.Add(offer);
        }
    }
}
