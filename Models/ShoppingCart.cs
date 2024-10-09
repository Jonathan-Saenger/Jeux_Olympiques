using Jeux_Olympiques.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Jeux_Olympiques.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;


namespace Jeux_Olympiques.Models
{
    /// <summary>
    /// Déclaration de la classe partielle ShoppingCart pour gérer le panier d'achat.
    /// </summary>
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
        public static ShoppingCart GetCart(HttpContext context, ApplicationDbContext dbContext)
        {
            var cart = new ShoppingCart(dbContext);
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        /// <summary>
        /// Méthode de simplification des appels du panier
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static ShoppingCart GetCart(Controller controller, ApplicationDbContext dbContext)
        {
            return GetCart(controller.HttpContext, dbContext);
        }
        /// <summary>
        /// Rechercher les instances du panier et de l'offre correspondante
        /// </summary>
        /// <param name="offer"></param>
        public void AddToCart(Offer offer)
        {
            var cartItem = _context?.Carts?.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.OfferId == offer.OfferId);

            if (cartItem == null)
            {
                cartItem = new Cart //Créer un nouveau panier si ce dernier est vide
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
            _context?.SaveChanges(); //Sauvegarder les changements
        }
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
                _context?.SaveChanges(); //Sauvagarde des changements
            }
            return itemQuantity;
        }
        public void EmptyCart()
        {
            var cartItems = _context?.Carts?.Where(
            cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context?.Carts?.Remove(cartItem);
            }
            _context?.SaveChanges(); //Sauvegarde des changements
        }
        public List<Cart> GetCartItems()
        {
            return _context.Carts
                .Include(c => c.Offer)
                .ThenInclude(o => o.Events)
                .Where(c => c.CartId == ShoppingCartId)
                .ToList();
        }
        public int GetQuantity() //Récupérer la quantité de chaque item dans le panier
        {
            int? quantity = (from cartItems in _context.Carts
                             where cartItems.CartId == ShoppingCartId
                             select (int?)cartItems.Quantity).Sum();
            return quantity ?? 0; //Retourner 0 si toutes les entrées sont nulles
        }
        public decimal GetTotal() // Multiplier le prix de chaque album par sa quantité pour obtenir le total du panier.
        {
            decimal? total = (from cartItems in _context?.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Quantity *
                              cartItems.Offer.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateTicket(Ticket ticket)
        {
            decimal ticketTotal = 0;

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
                ticketTotal += item.Quantity * item.Offer.Price ?? 0; //Définir le total du panier

                _context.TicketDetails.Add(ticketDetail); // Ajout du ticketDetail dans la base de données
            }
            ticket.Price = ticketTotal;
            _context.SaveChanges();
            EmptyCart();
            return ticket.TicketId; // Renvoie le TicketId comme numéro de commande
        }
        /// <summary>
        /// On utilise HttpContext pour autoirser l'accès aux cookies
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

