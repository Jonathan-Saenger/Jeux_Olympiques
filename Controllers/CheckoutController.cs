using EllipticCurve.Utils;
using Jeux_Olympiques.Areas.Identity.Data;
using Jeux_Olympiques.Data;
using Jeux_Olympiques.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using QRCoder;
using System.Drawing;

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

        //
        // GET: /Checkout/AddressAndPayment
        /// <summary>
        /// Gère la requête GET pour afficher la page d'adresse et de paiement.
        /// </summary>
        /// <returns>
        /// La vue de la page d'adresse et de paiement ou  redirection vers la page du panier si vide.
        /// </returns>
        /// <remarks>
        /// Récupère le contenu du panier d'achat en utilisant le contexte HTTP et
        /// l'objet de contexte de la base de données. Si le panier est vide, l'utilisateur 
        /// est redirigé vers le panier. Sinon, un formulaire fictif est présenté pour finaliser la commande.
        /// </remarks>
        [HttpGet]
        public IActionResult AddressAndPayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _context);
            var cartItems = cart.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            return View(cart);
        }

        /// <summary>
        /// Génère une clé unique pour un ticket en utilisant l'algorithme de hachage SHA256.
        /// </summary>
        /// <param name="ticket">Le ticket pour lequel générer la clé unique.</param>
        /// <returns>Une chaîne de caractères représentant la clé unique du ticket.</returns>
        /// <remarks>
        /// La clé est générée en combinant plusieurs propriétés du ticket (ID, nom, prix, date)
        /// puis en appliquant un hachage SHA256 sur cette combinaison. Le résultat est converti 
        /// en une chaîne hexadécimale en minuscules.
        /// ATTENTION : Mise à jour régulière de l'algorithme !
        /// </remarks>
        private string GenerateTicketKey(Ticket ticket)
        {
            using var sha256 = SHA256.Create();
            var combinedValue = $"{ticket.TicketId}{ticket.Name}{ticket.Price}{ticket.TicketDate:yyyyMMddHHmmss}";
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedValue));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        // POST : /Checkout/CompletePayment
        /// <summary>
        /// Complète le processus de paiement pour l'utilisateur connecté.
        /// - Si le panier est vide, redirige l'utilisateur vers la page du panier.
        /// - Crée un nouveau panier si nécessaire.
        /// - Génère une clé unique pour le ticket.
        /// - Crée un ticket à partir des éléments du panier.
        /// - Redirige l'utilisateur vers la page d'affichage du ticket.
        /// </summary>
        /// <remarks>
        /// - La méthode vérifie si le panier contient des articles.
        /// - Si le panier est vide, l'utilisateur est redirigé vers la page du panier pour ajouter des articles.
        /// - Si le panier contient des articles, un ticket est généré et sauvegardé dans la base de données.
        /// - La clé du ticket est générée de manière unique à partir des informations du ticket.
        /// - Enfin, l'utilisateur est redirigé vers la page où il peut voir son ticket.
        /// Scénarios d'erreurs :
        /// - Si aucun utilisateur n'est connecté, la méthode retournera une erreur.
        /// - Si aucun article n'est trouvé dans le panier, l'utilisateur est redirigé sans erreur.
        /// </remarks>
        /// <returns>
        /// Redirection vers la page du ticket ou du panier.</returns>
        [HttpPost]
        public IActionResult CompletePayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _context);
            var cartItems = cart.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            var userId = User.Identity.Name;
            var ticket = new Ticket
            {
                Buyer = _context.Users.FirstOrDefault(u => u.UserName == userId),
                TicketDate = DateTime.Now
            };

            ticket.TicketKey = GenerateTicketKey(ticket);
            cart.CreateTicket(ticket);

            return RedirectToAction("Ticket", new { ticketId = ticket.TicketId });
        }

        // GET: /Checkout/Ticket
        /// <summary>
        /// Récupère et affiche les informations d'un ticket client spécifié par son identifiant. 
        /// Génère également un code QR contenant les détails du ticket.
        /// </summary>
        /// <param name="ticketId">Identifiant unique du ticket à récupérer.</param>
        /// <remarks>
        /// Cette méthode effectue les actions suivantes :
        /// - Recherche du ticket dans la base de données en incluant les détails du ticket, les offres associées, 
        ///   les événements liés à ces offres, et les informations de l'acheteur.
        /// - Si le ticket n'existe pas, redirige l'utilisateur vers la page du panier.
        /// - Génération d'un QRCode contenant les informations essentielles du ticket (acheteur, place, offre, 
        ///   prix, date, etc.), qui est ensuite affiché dans la vue.
        /// Notes :
        /// - La génération du QRCode se fait via la bibliothèque `QRCoder`, qui doit être régulièrement mise à jour
        ///   pour garantir la compatibilité et la sécurité.
        /// - En cas de ticket non trouvé, l'utilisateur est redirigé vers le panier.
        /// </remarks>
        /// <returns>
        /// Retourne une vue contenant les détails du ticket et un code QR généré.
        /// Si le ticket n'existe pas, redirige vers la page du panier.
        /// </returns>
        [HttpGet]
        public IActionResult Ticket(int ticketId)
        {
            var ticket = _context.Tickets
                .Include(t => t.TicketDetails)
                .ThenInclude(td => td.Offer)
                .ThenInclude(o => o.Events)
                .Include(t => t.Buyer)
                .Include(c => c.Contains)
                .FirstOrDefault(t => t.TicketId == ticketId);

            if (ticket == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            var firstTicketDetail = ticket.TicketDetails.FirstOrDefault();
            
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode($@"Voici les donnees du Ticket de {ticket.Buyer.FirstName} {ticket.Buyer.LastName} :
            Place: {firstTicketDetail.Offer?.Place},
            Offre : {firstTicketDetail.Offer?.Description},
            Prix: {ticket.Price} €,
            Date: {ticket.TicketDate:dd/MM/yyyy HH:mm},
            Email : {ticket.Buyer}
            Cle Client: {ticket.Buyer.AccountKey},
            Cle Ticket: {ticket.TicketKey},
            Merci de bien verifier l'identite du detenteur du ticket",QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            ViewBag.QRCodeImage = qrCodeImage;

            return View(ticket);
        }
    }
}
