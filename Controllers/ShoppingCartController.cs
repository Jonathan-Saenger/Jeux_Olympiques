using Jeux_Olympiques.Data;
using Jeux_Olympiques.Models;
using Jeux_Olympiques.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Jeux_Olympiques.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _cart;
        public ShoppingCartController(ApplicationDbContext context, ShoppingCart cart)
        {
            _context = context;
            _cart = cart;
        }

        //GET: /ShoppingCart/
        public ActionResult Index()
        {
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = _cart.GetCartItems(),
                CartTotal = _cart.GetTotal()
            };
            return View(viewModel);
        }

        // GET: /Store/AddToCart/5
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
        public ActionResult CartSummary()
        {
            ViewData["CartCount"] = _cart.GetQuantity();
            return PartialView("CartSummary");
        }
    }
}
