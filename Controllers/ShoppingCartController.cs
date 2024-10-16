using Jeux_Olympiques.Data;
using Jeux_Olympiques.Models;
using Jeux_Olympiques.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Jeux_Olympiques.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _cart;

        /// <summary>
        /// Initialitialisation du contrôleur avec les injections de dépendances
        /// </summary>
        /// <remarks>
        /// Le constructeur injecte les services nécessaires pour gérer les opérations liées au panier d'achat :
        /// le contexte pour interagir avec la base de données et l'instance de panier pour les opérations spécifiques.
        /// </remarks>
        public ShoppingCartController(ApplicationDbContext context, ShoppingCart cart)
        {
            _context = context;
            _cart = cart;
        }

        //GET: /ShoppingCart/
        /// <summary>
        /// Affiche la vue du panier d'achat avec les articles.
        /// </summary>
        /// <remarks>
        /// Récupère les éléments actuels du panier ainsi que le total du panier à partir de l'instance de `ShoppingCart`.
        /// Ces informations sont ensuite encapsulées dans un `ShoppingCartViewModel` qui est passé à la vue pour l'affichage.
        /// </remarks>
        /// <returns>Vue du panier avec les articles et le total actuel.</returns>
        public async Task<ActionResult> Index()
        {
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = _cart.GetCartItems(),
                CartTotal = _cart.GetTotal()
            };
            return View(viewModel);
        }

        // GET: /Store/AddToCart/5
        /// <summary>
        /// Ajoute une offre au panier.
        /// </summary>
        /// <remarks>
        /// Cette méthode cherche l'offre spécifiée par son ID dans la base de données. Si l'offre est trouvée, elle est ajoutée au panier.
        /// Si l'offre n'est pas trouvée, une réponse `NotFound` est renvoyée. Après l'ajout, l'utilisateur est redirigé vers l'index du panier.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à ajouter au panier.</param>
        /// <returns>Redirection vers l'index du panier ou une erreur si l'offre n'est pas trouvée.</returns>
        public async Task<ActionResult> AddToCart(int id)
        {
            var addedOffer = await _context.Offers.SingleAsync(offer => offer.OfferId == id);
            if (addedOffer == null)
            {
                return NotFound();
            }

            _cart.AddToCart(addedOffer);

            return RedirectToAction("Index");
        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        /// <summary>
        /// AJAX pour supprimer un article du panier
        /// </summary>
        /// <remarks>
        /// Récupère l'article du panier par son `RecordId` et, s'il est trouvé, le supprime.
        /// La méthode renvoie un objet JSON avec les informations mises à jour du panier, y compris le nombre d'articles restants,
        /// le total du panier et un message confirmant la suppression. La suppression est effectuée via AJAX, 
        /// ce qui permet de mettre à jour l'interface en temps réelle et sans temps de chargement. 
        /// </remarks>
        /// <param name="id">Identifiant de l'article à supprimer du panier.</param>
        /// <returns>Un objet JSON contenant le résultat de l'opération </returns>
        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            var cartItem = await _context.Carts
               .Include(c => c.Offer) 
               .SingleOrDefaultAsync(item => item.RecordId == id);

            if (cartItem == null)
            {
                return NotFound();
            }

            string offerName = cartItem.Offer?.Title ?? "Offre inconnue"; 
            int itemQuantity = _cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = WebUtility.HtmlEncode(offerName) +
                    " a été retiré de votre panier d'achat.",
                CartTotal = _cart.GetTotal(),
                CartCount = _cart.GetQuantity(),
                ItemCount = itemQuantity,
                DeleteId = id
            };
            return Json(results);
        }

        //GET: /ShoppingCart/CartSummary
        /// <summary>
        /// Affiche un résumé du panier dans la barre de navigation.
        /// </summary>
        /// <remarks>
        /// Cette méthode renvoie une vue partielle (CartSummaryViewComponent) affiche le nombre d'articles
        /// actuel dans le panier ainsi que le montant total du panier. Il est actualisé en temps réel grâce
        /// à une requête AJAX. 
        /// </remarks>
        /// <returns>Vue partielle du résumé du panier avec le nombre d'articles et le montant total.</returns>

        public ActionResult CartSummary()
        {
            var cart = _context.Carts;
            ViewData["CartCount"] = _cart.GetQuantity();
            return PartialView("CartSummary");
        }
    }
}
