using Jeux_Olympiques.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Jeux_Olympiques.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;


namespace Jeux_Olympiques.Models
{
    /// <summary>
    /// Classe partielle ShoppingCart pour gérer les opérations liées au panier d'achat.
    /// </summary>
    /// <remarks>
    /// Cette classe fournit des méthodes pour ajouter, supprimer et gérer les articles dans le panier.
    /// Elle est aussi responsable de la gestion des sessions utilisateur, de la création des tickets, et du calcul du total du panier.
    /// </remarks>
    public partial class ShoppingCart
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }
        string ShoppingCartId { get; set; }
        /// <summary>
        /// Clé constante pour identifier le panier dans la session.
        /// </summary>
        public const string CartSessionKey = "CartId";

        /// <summary>
        /// Récupère le panier d'achat associé à l'utilisateur actuel à partir de la base de donnée.
        /// </summary>
        public static ShoppingCart GetCart(HttpContext context, ApplicationDbContext dbContext)
        {
            var cart = new ShoppingCart(dbContext);
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller, ApplicationDbContext dbContext)
        {
            return GetCart(controller.HttpContext, dbContext);
        }
        /// <summary>
        /// Ajoute l'offre spécifiée au panier d'achat.
        /// </summary>
        /// <param name="offer">L'offre à ajouter au panier.</param>
        /// <remarks>
        /// Si l'offre existe déjà dans le panier, la quantité est incrémentée.
        /// Si l'offre n'existe pas, un nouvel élément est créé dans le panier.
        /// </remarks>
        public void AddToCart(Offer offer)
        {
            var cartItem = _context?.Carts?.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.OfferId == offer.OfferId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    OfferId = offer.OfferId,
                    CartId = ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _context?.Carts?.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++; //Si le panier est vide, ajouter une offre
            }
            _context?.SaveChanges();
        }

        /// <summary>
        /// Suppression d'une offre du panier.
        /// </summary>
        /// <param name="id">Identifiant de l'élément à supprimer.</param>
        /// <returns>La quantité restante de l'élément dans le panier, ou 0 si l'élément est supprimé.</returns>
        /// <remarks>
        /// Si la quantité de l'élément est supérieure à 1, celle-ci est décrémentée. Sinon, l'élément est supprimé du panier.
        /// </remarks>
        public int RemoveFromCart(int id)
        {
            var cartItem = _context?.Carts?.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id); //Récupérer le panier

            int itemQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemQuantity = cartItem.Quantity;
                }
                else
                {
                    _context?.Carts?.Remove(cartItem);
                }
                _context?.SaveChanges();
            }
            return itemQuantity;
        }

        /// <summary>
        /// Vide le panier
        /// </summary>
        /// <remarks>
        /// Cette méthode supprime tous les éléments du panier et sauvegarde les modifications dans la base de données.
        /// </remarks>
        public void EmptyCart()
        {
            var cartItems = _context?.Carts?.Where(
            cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context?.Carts?.Remove(cartItem);
            }
            _context?.SaveChanges();
        }


        /// <summary>
        /// Récupère tous les articles dans le panier actuel.
        /// </summary>
        /// <returns>Liste d'éléments de type <see cref="Cart"/> présents dans le panier.</returns>
        /// <remarks>
        /// Requête LINQ avec chargement anticipé (Eager Loading) pour inclure les offres et les événements 
        /// associés à chaque élément du panier.
        /// </remarks>
        public List<Cart> GetCartItems()
        {
            return _context.Carts
                .Include(c => c.Offer)
                .ThenInclude(o => o.Events)
                .Where(c => c.CartId == ShoppingCartId)
                .ToList();
        }

        /// <summary>
        /// Calcule le total d'articles dans le panier.
        /// </summary>
        /// <returns>Le nombre total d'articles dans le panier.</returns>
        /// <remarks>
        /// Si le panier est vide,  retourne 0.
        /// </remarks>
        public int GetQuantity()
        {
            int? quantity = (from cartItems in _context.Carts
                             where cartItems.CartId == ShoppingCartId
                             select (int?)cartItems.Quantity).Sum();
            return quantity ?? 0; 
        }

        /// <summary>
        /// Prix total des articles dans le panier.
        /// </summary>
        /// <returns>Le prix total des articles dans le panier.</returns>
        /// <remarks>
        /// Si aucun article n'est présent dans le panier, cette méthode retourne 0.
        /// </remarks>
        public decimal GetTotal()
        {
            decimal? total = (from cartItems in _context?.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Quantity *
                              cartItems.Offer.Price).Sum();

            return total ?? decimal.Zero;
        }

        /// <summary>
        /// Crée un ticket (commande client)
        /// </summary>
        /// <returns>L'ID du ticket créé.</returns>
        /// <remarks>
        /// Cette méthode génère un ticket avec les détails du panier. Le panier ainsi que la vue partielle 
        /// du ticket sont vidés après la création du ticket.
        /// </remarks>
        public int CreateTicket(Ticket ticket)
        {
            decimal ticketTotal = 0;

            _context.Tickets.Add(ticket);
            _context.SaveChanges(); 

            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                var ticketDetail = new TicketDetail
                {
                    OfferId = item.OfferId,
                    TicketId = ticket.TicketId,
                    UnitPrice = item?.Offer?.Price ?? 0,
                    Count = item.Quantity
                };
                ticketTotal += item.Quantity * item.Offer.Price ?? 0;

                _context.TicketDetails.Add(ticketDetail);
            }
            ticket.Price = ticketTotal;
            _context.SaveChanges();
            EmptyCart();
            return ticket.TicketId;
        }

        /// <summary>
        /// Récupère l'identifiant du panier depuis la session utilisateur.
        /// </summary>
        /// <returns>L'ID du panier sous forme de chaîne de caractères.</returns>
        /// <remarks>
        /// Si l'ID du panier n'existe pas dans la session, un nouvel ID est généré et stocké.
        /// </remarks>
        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString(CartSessionKey, context.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return context.Session.GetString(CartSessionKey);
        }

        /// <summary>
        /// Migre les articles du panier d'un visiteur vers un utilisateur authentifié.
        /// </summary>
        /// <remarks>
        /// Cette méthode met à jour l'ID du panier pour qu'il soit associé à l'utilisateur inscrit après l'authetification.
        /// </remarks>
        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Carts?.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _context.SaveChanges();
        }
    }
}

