using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jeux_Olympiques.Data;
using Jeux_Olympiques.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jeux_Olympiques.Controllers
{
    public class OffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Offers
        /// <summary>
        /// Récupère et affiche la liste des offres avec les événements associés.
        /// </summary>
        /// <returns>Charge les offres et les événements reliés depuis la base de données et les affiche dans une vue.</returns>
        public async Task<IActionResult> Index()
        {
            var offers = await _context.Offers
           .Include(o => o.Events)
           .ToListAsync();
            return View(offers);
        }

        // GET: Offers/Details/5
        /// <summary>
        /// Affiche les détails d'une offre spécifique.
        /// </summary>
        /// <remarks>
        /// Récupère une offre par son ID, en incluant les événements associés.
        /// Redirige vers une page "NotFound" si l'offre est introuvable.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre.</param>
        /// <returns>Vue des détails de l'offre ou erreur si non trouvée.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.Events)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // GET: Offers/Create
        /// <summary>
        /// Affiche le formulaire pour créer une nouvelle offre.
        /// </summary>
        /// <remarks>
        /// Génère une liste déroulante avec les événements disponibles pour lier à l'offre.
        /// L'utilisateur doit avoir le rôle d'Admin pour accéder à cette action.
        /// </remarks>
        /// <returns>Vue du formulaire de création.</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var events = _context.Events
                .Select(e => new {
                    Id = e.Id,
                    DisplayText = $"{e.Entitled} - le {e.Date:dd/MM/yyyy} - à {e.Site}"
                })
                .ToList();

            ViewData["EventId"] = new SelectList(events, "Id", "DisplayText");
            return View();
        }

        // POST: Offers/Create
        /// <summary>
        /// Crée une nouvelle offre dans la base de données.
        /// </summary>
        /// <remarks>
        /// Valide les données du formulaire avant d'ajouter l'offre.
        /// Redirige vers la liste des offres après la création, ou renvoie le formulaire en cas d'erreur.
        /// Nécessite un rôle Admin pour cette action.
        /// Référence de .NET, consultez : http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </remarks>
        /// <param name="offer">Les détails de l'offre à créer.</param>
        /// <returns>Redirection vers la liste des offres ou réaffichage du formulaire en cas d'erreur.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Place,Price,EventId")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var events = _context.Events
                .Select(e => new {
                    Id = e.Id,
                    DisplayText = $"{e.Entitled} - le {e.Date:dd/MM/yyyy} - à {e.Site}" 
                })
                .ToList();

            ViewData["EventId"] = new SelectList(events, "Id", "DisplayText", offer.EventId); 
            return View(offer);
        }

        // GET: Offers/Edit/5
        /// <summary>
        /// Affiche le formulaire pour modifier une offre.
        /// </summary>
        /// <remarks>
        /// L'utilisateur doit être authentifié avec le rôle admin pour cette action. 
        /// Charge l'offre existante par son ID. Si elle n'existe pas, une page "NotFound" est renvoyée.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à modifier.</param>
        /// <returns>Vue du formulaire d'édition ou erreur si l'offre n'est pas trouvée.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            return View(offer);
        }

        // POST: Offers/Edit/5
        /// <summary>
        /// Met à jour une offre existante.
        /// </summary>
        /// <remarks>
        /// Valide et met à jour les informations de l'offre. En cas de conflit lors de la mise à jour,
        /// vérifie si l'offre existe encore. Nécessite un rôle Admin pour cette action.
        /// Plus de détails .NEt : http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à mettre à jour.</param>
        /// <param name="offer">Les détails mis à jour de l'offre.</param>
        /// <returns>Redirection vers la liste des offres ou réaffichage du formulaire en cas d'erreur.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Place,Price")] Offer offer)
        {
            if (id != offer.OfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.OfferId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }

        // GET: Offers/Delete/5
        /// <summary>
        /// Affiche le formulaire de suppression d'une offre.
        /// </summary>
        /// <remarks>
        /// Cette méthode nécessite que l'utilisateur soit authentifié avec le rôle "Admin".
        /// Récupère l'offre à supprimer par son ID. Si elle n'existe pas, redirige vers "NotFound".
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à supprimer.</param>
        /// <returns>Vue du formulaire de suppression ou erreur si l'offre n'est pas trouvée.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        /// <summary>
        /// Confirme la suppression d'une offre.
        /// </summary>
        /// <remarks>
        /// Cette méthode est protégée contre les attaques CSRF (Cross-Site Request Forgery)/
        /// L'utilisateur doit authentifié avec le rôle "Admin" pour cette action. Supprime l'offre 
        /// sélectionnée et la supprime de la base de données. Redirige vers la liste des offres après 
        /// suppression.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à supprimer.</param>
        /// <returns>Redirection vers la liste des offres après suppression.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.OfferId == id);
        }
    }
}
