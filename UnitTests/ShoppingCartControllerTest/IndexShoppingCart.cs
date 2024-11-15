using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeux_Olympiques.Tests
{
    /// <summary>
    /// Classe de test pour le contrôleur du panier d'achat (ShoppingCartController).
    /// On teste ici les méthodes pour vérifier la gestion du panier avec ou sans articles par le controller.
    /// </summary>
    [TestClass]
    public class ShoppingCartControllerTests
    {
        /// <summary>
        /// Test pour vérifier que la vue retournée contient des articles dans le panier ainsi que le total correct
        /// </summary>
        [TestMethod]
        public async Task Index_WithItemsInCart_ReturnsViewWithItemsAndTotal()
        {
            // Arrange : Initialisation du contrôleur avec un panier qui contient des articles
            var controller = new ShoppingCartController(new TestShoppingCart());

            // Act : 
            var result = await controller.Index();

            // Assert : Vérification que le résultat est une vue du panier avec les articles et le total.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(ShoppingCartViewModel));
            var model = (ShoppingCartViewModel)viewResult.Model;

            Assert.IsNotNull(model.CartItems);
            Assert.AreEqual(2, model.CartItems.Count);
            Assert.AreEqual(150.00m, model.CartTotal);
        }

        /// <summary>
        /// Test pour vérifier que la vue retournée contient un panier vide et un total de zéro
        /// </summary>
        [TestMethod]
        public async Task Index_WithEmptyCart_ReturnsViewWithEmptyCartAndZeroTotal()
        {
            // Arrange : Initialisation du contrôleur avec un panier vide
            var controller = new ShoppingCartController(new TestEmptyShoppingCart());

            // Act : 
            var result = await controller.Index();

            // Assert : Panier vide et un total de 0.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;

            Assert.IsInstanceOfType(viewResult.Model, typeof(ShoppingCartViewModel));
            var model = (ShoppingCartViewModel)viewResult.Model;

            Assert.IsNotNull(model.CartItems);
            Assert.AreEqual(0, model.CartItems.Count);
            Assert.AreEqual(0.00m, model.CartTotal);
        }

        /// <summary>
        /// Contrôleur simulé pour gérer le panier d'achat
        /// </summary>
        public class ShoppingCartController
        {
            private readonly IShoppingCart _cart;

            /// <summary>
            /// Constructeur du contrôleur qui prend en paramètre un panier d'achat.
            /// </summary>
            /// <param name="cart">Instance du panier d'achat simulé.</param>
            public ShoppingCartController(IShoppingCart cart)
            {
                _cart = cart;
            }

            /// <summary>
            /// Méthode Index qui retourne une vue avec les articles du panier et le total.
            /// </summary>
            /// <returns>Vue contenant le modèle du panier d'achat.</returns>
            public async Task<ActionResult> Index()
            {
                var viewModel = new ShoppingCartViewModel
                {
                    CartItems = await _cart.GetCartItems(),
                    CartTotal = await _cart.GetTotal()
                };
                return new ViewResult { Model = viewModel };
            }
        }

        /// <summary>
        /// Interface du panier d'achat, utilisée pour simuler les opérations sur le panier.
        /// </summary>
        public interface IShoppingCart
        {
            Task<List<Cart>> GetCartItems();
            Task<decimal> GetTotal();
        }

        /// <summary>
        /// Simulation d'un panier d'achat contenant des articles pour le test.
        /// </summary>
        public class TestShoppingCart : IShoppingCart
        {
            /// <summary>
            /// Simule la récupération d'une liste d'articles dans le panier.
            /// </summary>
            /// <returns>Liste contenant deux articles fictifs.</returns>
            public Task<List<Cart>> GetCartItems()
            {
                return Task.FromResult(new List<Cart>
                {
                    new Cart { RecordId = 1, Quantity = 2, Price = 50.00m, DateCreated = DateTime.Now, Offer = new Offer { Id = 1, Name = "Offer 1" }, CartId = "cart1" },
                    new Cart { RecordId = 2, Quantity = 1, Price = 50.00m, DateCreated = DateTime.Now, Offer = new Offer { Id = 2, Name = "Offer 2" }, CartId = "cart1" }
                });
            }
            public Task<decimal> GetTotal()
            {
                return Task.FromResult(150.00m);
            }
        }

        /// <summary>
        /// Simulation d'un panier vide pour le test.
        /// </summary>
        public class TestEmptyShoppingCart : IShoppingCart
        {
            public Task<List<Cart>> GetCartItems()
            {
                return Task.FromResult(new List<Cart>());
            }

            public Task<decimal> GetTotal()
            {
                return Task.FromResult(0.00m);
            }
        }

        /// <summary>
        /// Vue du panier d'achat, on affiche les articles et le total dans la vue.
        /// </summary>
        public class ShoppingCartViewModel
        {
            public List<Cart> CartItems { get; set; } 
            public decimal CartTotal { get; set; } 
        }

        /// <summary>
        /// Simulation de la classe représentant un article dans le panier.
        /// </summary>
        public class Cart
        {
            public int RecordId { get; set; } 
            public int Quantity { get; set; } 
            public decimal Price { get; set; } 
            public DateTime DateCreated { get; set; } 
            public Offer Offer { get; set; } 
            public string CartId { get; set; } 
        }

        /// <summary>
        /// Classe représentant une offre dans le panier.
        /// </summary>
        public class Offer
        {
            public int Id { get; set; } // ID de l'offre
            public string Name { get; set; } // Nom de l'offre
        }

        /// <summary>
        /// Classe simulée pour représenter une vue résultat, avec un modèle associé.
        /// </summary>
        public class ViewResult : ActionResult
        {
            public object Model { get; set; } 
        }
        public class ActionResult { }
    }
}
