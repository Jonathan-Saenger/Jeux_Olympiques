using Jeux_Olympiques.Areas.Identity.Data;
using Jeux_Olympiques.Data;
using Jeux_Olympiques.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jeux_Olympiques.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        private readonly ApplicationDbContext _context;     

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        const string PromoCode = "PAIEMENT";
        //
        // GET: /Checkout/AdressAndPayment
        public IActionResult AdresseAndPayment()
        {
            return View();
        }

        //POST: /Checkout/AdressAndPayment
        [HttpPost]
        public async Task<IActionResult> AdressAndPayment(IFormCollection values)
        {
            var ticket = new Ticket();
            await TryUpdateModelAsync(ticket);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(ticket);
                }
                else
                {
                    ticket.Buyer = Jeux_OlympiquesUser.Name;
                    ticket.TicketDate = DateTime.Now;

                    _context.Tickets.Add(ticket);
                    await _context.SaveChangesAsync();

                    var cart = ShoppingCart.GetCart(HttpContext, _context);
                    cart.CreateTicket(ticket);

                    return RedirectToAction("Complete", new { id = ticket.TicketId });
                }
            }
            catch
            {
                return View(ticket);
            }
        }


        /// <summary>
        /// Cette API vérifie que la commande appartient bien à l'utilisateur connecté avant d'afficher le numéro de commande de confirmation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> CompleteAsync(int id)
        {
            //Valider que le client est propriétaire de cette commande 
            bool isValid = await _context.Tickets.AnyAsync(
                o => o.TicketId == id && o.Buyer == Jeux_OlympiquesUser.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
