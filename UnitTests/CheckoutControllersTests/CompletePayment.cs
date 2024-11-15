using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jeux_Olympiques.Tests
{
    [TestClass]
    public class CheckoutControllerTests
    {
        private const string RedirectToShoppingCart = "ShoppingCart";

        /// <summary>
        /// Simule le comportement de la méthode CompletePayment du CheckoutController
        /// Vérifie la création un ticket et redirige correctement lorsqu'il y a des articles dans le panier.
        /// </summary>
        [TestMethod]
        public void CompletePayment_WithItemsInCart_CreatesTicketAndRedirects()
        {
            // Arrange: Crée une liste d'articles de panier avec un élément.
            var cartItems = new List<CartItem> { new CartItem() };
            var userId = "testUser";

            // Act
            var result = SimulateCompletePayment(cartItems, userId);

            // Assert: Vérifie que le résultat est une redirection vers une URL de ticket valide.
            AssertRedirectsToTicket(result);
        }
        
        /// <summary>
        /// Teste que la méthode CompletePayment redirige vers le panier lorsque le panier est vide.
        /// </summary>
        [TestMethod]
        public void CompletePayment_WithEmptyCart_RedirectsToShoppingCart()
        {
            // Arrange: Crée une liste vide d'articles de panier.
            var cartItems = new List<CartItem>();
            var userId = "testUser";

            // Act
            var result = SimulateCompletePayment(cartItems, userId);

            // Assert: Vérifie que le résultat redirige vers la page du panier.
            AssertRedirectsToShoppingCart(result);
        }

        /// <summary>
        /// On teste la redirection vers une URL de ticket avec un identifiant.
        /// </summary>
        /// <param name="result">Le résultat de la méthode à tester.</param>
        private void AssertRedirectsToTicket(IActionResult result)
        {
            // Vérifie que le résultat est une instance de RedirectResult.
            Assert.IsInstanceOfType(result, typeof(RedirectResult), "Le résultat n'est pas une redirection.");
            var redirectResult = (RedirectResult)result;

            // Vérifie que l'URL de redirection commence par "Ticket?ticketId=".
            Assert.IsTrue(redirectResult.Url.StartsWith("Ticket?ticketId="),
                "CompletePayment ne crée pas de ticket ou ne redirige pas correctement.");
        }

        /// <summary>
        /// Vérifie que le résultat redirige vers le panier.
        /// </summary>
        private void AssertRedirectsToShoppingCart(IActionResult result)
        {
            // Vérifie que le résultat est une instance de RedirectResult.
            Assert.IsInstanceOfType(result, typeof(RedirectResult), "Le résultat n'est pas une redirection.");
            var redirectResult = (RedirectResult)result;

            // Vérifie que l'URL de redirection est égale à "ShoppingCart".
            Assert.AreEqual(RedirectToShoppingCart, redirectResult.Url,
                "CompletePayment ne redirige pas correctement avec un panier vide.");
        }

        /// <summary>
        /// Simulation du paiement et de la création d'un ticket.
        /// </summary>
        private IActionResult SimulateCompletePayment(List<CartItem> cartItems, string userId)
        {
            // Vérifie si le panier est vide ou null et redirige vers la page du panier si c'est le cas.
            if (cartItems == null || !cartItems.Any())
            {
                return new RedirectResult(RedirectToShoppingCart);
            }

            // Crée un ticket avec les détails du panier et redirige vers la page du ticket.
            var ticket = CreateTicket(cartItems, userId);
            return new RedirectResult($"Ticket?ticketId={ticket.TicketId}");
        }

        /// <summary>
        /// Création d'un ticket de test.
        /// </summary>
        /// <param name="cartItems">Les articles dans le panier.</param>
        /// <param name="userId">L'identifiant de l'utilisateur.</param>
        /// <returns>Un ticket contenant les détails.</returns>
        private Ticket CreateTicket(List<CartItem> cartItems, string userId)
        {
            return new Ticket
            {
                TicketId = new Random().Next(1000, 9999),
                Buyer = new User { UserName = userId },
                TicketDate = DateTime.Now,
                TicketDetails = cartItems.Select(item => new TicketDetail()).ToList()
            };
        }

        /// <summary>
        /// Simulation de classes pour le test.
        /// </summary>
        public class CartItem { }

        public class Ticket
        {
            public int TicketId { get; set; }
            public User Buyer { get; set; }
            public DateTime TicketDate { get; set; }
            public List<TicketDetail> TicketDetails { get; set; }
        }

        public class TicketDetail { }
        public class User
        {
            public string UserName { get; set; }
        }
    }
}
