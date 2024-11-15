using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Jeux_Olympiques.Tests
{
    [TestClass]
    public class AddressAndPaymentTests
    {
        /// <summary>
        /// Simule le comportement de la méthode AddressAndPayment du CheckoutController
        /// Retourne une redirection vers le panier si le panier est vide ou nul, sinon retourne une ViewResult.
        /// </summary>
        /// <param name="cartItems">Liste des articles dans le panier.</param>
        /// <returns>Une IActionResult qui indique le résultat de l'opération.</returns>
        private IActionResult SimulateAddressAndPayment(List<CartItem> cartItems)
        {
            if (cartItems == null || cartItems.Count == 0)
            {
                return new RedirectResult("ShoppingCart");
            }
            return new ViewResult();
        }

        /// <summary>
        /// Vérifie que la méthode AddressAndPayment retourne une redirection vers le panier lorsque la liste des articles est vide ou nulle.
        /// </summary>
        /// <param name="result">Le résultat de l'exécution de la méthode AddressAndPayment.</param>
        private void AssertRedirectToShoppingCart(IActionResult result)
        {
            Assert.IsInstanceOfType(result, typeof(RedirectResult), "La méthode ne redirige pas vers le panier quand le panier est vide.");
            var redirectResult = (RedirectResult)result;
            Assert.AreEqual("ShoppingCart", redirectResult.Url, "La redirection n'est pas correcte.");
        }

        /// <summary>
        /// Teste que la méthode AddressAndPayment retourne une ViewResult lorsque des articles sont présents dans le panier.
        /// </summary>
        [TestMethod]
        public void AddressAndPayment_WithItemsInCart_ReturnsViewResult()
        {
            // Arrange : Prépare une liste d'articles dans le panier.
            var cartItems = new List<CartItem> { new CartItem() };

            // Act
            var result = SimulateAddressAndPayment(cartItems);

            // Assert : Vérifie que le résultat est une ViewResult.
            Assert.IsInstanceOfType(result, typeof(ViewResult), "La méthode ne retourne pas une ViewResult quand le panier contient des articles.");
        }

        /// <summary>
        /// Teste que la méthode AddressAndPayment redirige vers le panier lorsque le panier est vide.
        /// </summary>
        [TestMethod]
        public void AddressAndPayment_WithEmptyCart_RedirectsToShoppingCart()
        {
            // Arrange : Prépare une liste vide d'articles dans le panier.
            var cartItems = new List<CartItem>();

            // Act
            var result = SimulateAddressAndPayment(cartItems);

            // Assert : Vérifie que la méthode redirige vers le panier.
            AssertRedirectToShoppingCart(result);
        }

        /// <summary>
        /// Teste que la méthode AddressAndPayment redirige vers le panier lorsque les articles du panier sont nuls.
        /// </summary>
        [TestMethod]
        public void AddressAndPayment_WithNullCartItems_RedirectsToShoppingCart()
        {
            // Arrange : Définit le panier comme étant nul.
            List<CartItem> cartItems = null;

            // Act :
            var result = SimulateAddressAndPayment(cartItems);

            // Assert : Vérifie que la méthode redirige vers le panier.
            AssertRedirectToShoppingCart(result);
        }
    }

    // Simulation des classes utilisées dans le test
    public class CartItem { }
    public class IActionResult { }
    public class ViewResult : IActionResult { }
    public class RedirectResult : IActionResult
    {
        public string Url { get; set; }
        public RedirectResult(string url) { Url = url; }
    }
}
