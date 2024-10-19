// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeux_Olympiques.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Modèle de page utilisé pour confirmer l'adresse email d'un utilisateur lors de son inscription.
    /// Ce processus est essentiel pour valider la propriété de l'email fourni et garantir que l'utilisateur
    /// peut recevoir la notification d'inscription.
    /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour personnaliser
    /// l'utilisateur. Cette dernière est amené à évoluer en fonction des futures mise à jour
    /// </summary>
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<Jeux_OlympiquesUser> _userManager;

        public ConfirmEmailModel(UserManager<Jeux_OlympiquesUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Méthode appelée pour confirmer l'email d'un utilisateur. L'email sera confirmé 
        /// et un message de succès sera affiché. A défaut, un message d'erreur sera affiché.
        /// Le message a été ici personnalisé.
        /// </summary>
        /// <param name="userId">Identifiant de l'utilisateur.</param>
        /// <param name="code">Code de confirmation de l'email.</param>
        /// <returns>Retourne la page actuelle avec le message d'état, ou une redirection en cas d'échec.</returns>
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "La vérification de votre mail a bien été effectuée, à très vite sur le site des Jeux Olympiques !" : "Une erreur est survenue lors de la vérification de votre mail !";
            return Page();
        }
    }
}
