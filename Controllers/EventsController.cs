﻿using System;
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
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Events/Details/5
        /// <summary>
        /// Afficher les détails d'un événement précis.
        /// </summary>
        /// <param name="id">L'identifiant de l'événement à afficher.</param>
        /// <returns>
        /// La vue des détails de l'événement ou une réponse NotFound si l'événement n'existe pas.
        /// </returns>
        /// <remarks>
        /// Vérifie si l'identifiant fourni est nul (renvoie une réponse NotFound).
        /// Sinon, elle tente de récupérer l'événement à partir du contexte de la base de données, 
        /// en incluant les offres associées à l'événement. Si l'événement est trouvé, la vue des 
        /// détails de l'événement est renvoyée, sinon une réponse NotFound est renvoyée.
        /// </remarks>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Offers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        /// <summary>
        /// Créer un nouvel événement.
        /// </summary>
        /// <param name="@event">L'objet Event contenant les informations à enregistrer.</param>
        /// <returns>
        /// Redirige vers l'index des événements si la création est réussie 
        /// </returns>
        /// <remarks>
        /// Cette méthode est protégée contre les attaques de surpostage en liant uniquement 
        /// les propriétés spécifiées de l'objet Event. L'utilisateur doit être authentifié 
        /// avec le rôle "Admin". L'événement est ajouté au contexte et les modifications sont 
        /// enregistrées. Sinon, la vue est renvoyée avec l'objet Event pour permettre 
        /// à l'utilisateur de corriger les erreurs.
        /// Référence de .NET, consultez : http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Entitled,Presentation,Date,Site")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        /// <summary>
        /// Affiche le formulaire d'édition d'un événement spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'événement à éditer.</param>
        /// <returns>
        /// La vue du formulaire d'édition de l'événement, ou une réponse NotFound si l'événement n'existe pas.
        /// </returns>
        /// <remarks>
        /// L'utilisateur doit être authentifié avec le rôle "Admin". Cette méthode récupère l'événement 
        /// correspondant à l'identifiant depuis la base de données. Si l'événement est trouvé, la vue 
        /// du formulaire d'édition est renvoyée, sinon une réponse NotFound est également renvoyée.
        /// </remarks>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        /// <summary>
        /// Gère la requête POST pour mettre à jour un événement spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'événement à éditer.</param>
        /// <param name="@event">L'objet Event contenant les informations mises à jour.</param>
        /// <returns>
        /// Redirige vers l'index des événements si la mise à jour est réussie, ou renvoie la vue avec 
        /// le modèle en cas d'erreur de validation ou si l'événement n'existe pas.
        /// </returns>
        /// <remarks>
        /// Cette méthode est protégée et nécessite également que l'utilisateur soit authentifié avec le rôle "Admin".
        ///  Si l'identifiant fourni ne correspond pas à celui de l'événement dans le modèle, une réponse NotFound est renvoyée.
        /// Si l'état du modèle est valide, la méthode tente de mettre à jour l'événement dans le contexte. En cas d'échec
        ///  de la mise à jour due à une concurrence, elle vérifie si l'événement existe toujours. Si l'événement n'existe plus, 
        /// une réponse NotFound est renvoyée, sinon une exception est relancée.
        /// Pour plus de détails sur la protection contre le surpostage, consultez : http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Entitled,Presentation,Date,Site")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        /// <summary>
        /// Gère la requête GET pour afficher la page de confirmation de suppression d'un événement spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'événement à supprimer.</param>
        /// <returns>
        /// La vue de confirmation de suppression de l'événement, ou une réponse NotFound si l'événement n'existe pas.
        /// </returns>
        /// <remarks>
        /// Cette méthode nécessite que l'utilisateur soit authentifié avec le rôle "Admin". 
        /// Elle commence par vérifier si l'identifiant fourni est nul. Si c'est le cas, 
        /// elle renvoie une réponse NotFound. Ensuite, elle tente de récupérer l'événement 
        /// correspondant à l'identifiant à partir du contexte de la base de données.
        /// Si l'événement est trouvé, la vue de confirmation de suppression est renvoyée. 
        /// Sinon, une réponse NotFound est également renvoyée, indiquant que l'événement 
        /// à supprimer n'existe pas.
        /// </remarks>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        /// <summary>
        /// Gère la requête POST pour confirmer la suppression d'un événement spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'événement à supprimer.</param>
        /// <returns>
        /// L'index des événements après la suppression
        /// </returns>
        /// <remarks>
        /// Cette méthode est protégée contre les attaques CSRF (Cross-Site Request Forgery) 
        /// Elle nécessite également que l'utilisateur soit authentifié avec le rôle "Admin".
        /// Elle tente d'abord de trouver l'événement correspondant à l'identifiant 
        /// fourni. Si l'événement est trouvé, il est supprimé du contexte. 
        /// Quoi qu'il en soit, après l'opération, les changements sont enregistrés 
        /// dans la base de données. La méthode redirige ensuite vers l'index des 
        /// événements, que la suppression ait été effectuée ou que l'événement 
        /// n'ait pas été trouvé.
        /// </remarks>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
