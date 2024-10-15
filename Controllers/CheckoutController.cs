﻿using EllipticCurve.Utils;
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
        [HttpGet]
        public IActionResult AddressAndPayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _context);
            var cartItems = cart.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                //Redirection si le panier est vide
                return RedirectToAction("Index", "ShoppingCart");
            }
            //Affichage du formulaire fictif pour finaliser
            return View(cart);
        }

        // POST : /Checkout/CompletePayment
        [HttpPost]
        public IActionResult CompletePayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _context);
            var cartItems = cart.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                //Redirection si le panier est vide
                return RedirectToAction("Index", "ShoppingCart");
            }

            //Création du ticket fictif pour l'utilisateur connecté
            var userId = User.Identity.Name;
            var ticket = new Ticket
            {
                Buyer = _context.Users.FirstOrDefault(u => u.UserName == userId),
                TicketDate = DateTime.Now
            };

            //Générer la clé unique pour le ticket
            ticket.TicketKey = GenerateTicketKey(ticket);

            //Générer le ticket basé sur le panier
            cart.CreateTicket(ticket);

            //Redirection vers la page qui affiche le ticket
            return RedirectToAction("Ticket", new { ticketId = ticket.TicketId });
        }

        // GET: /Checkout/Ticket
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
            Prix: {ticket.Price},
            Date: {ticket.TicketDate:yyyy/MM/dd HH:mm},
            Email : {ticket.Buyer}
            Cle Client: {ticket.Buyer.AccountKey},
            Cle Ticket: {ticket.TicketKey},
            Merci de bien verifier l'identite du detenteur du ticket",QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            ViewBag.QRCodeImage = qrCodeImage;

            return View(ticket);
        }

        private string GenerateTicketKey(Ticket ticket)
        {
            using var sha256 = SHA256.Create();
            var combinedValue = $"{ticket.TicketId}{ticket.Name}{ticket.Price}{ticket.TicketDate:yyyyMMddHHmmss}";
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedValue));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
