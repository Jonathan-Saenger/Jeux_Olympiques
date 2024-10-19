// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Jeux_Olympiques.Areas.Identity.Data;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account
{

    /// <summary>
    /// Modèle de page pour la réinitialisation du mot de passe d'un utilisateur.
    /// Cette classe gère le processus de réinitialisation du mot de passe, y compris
    /// la validation des informations saisies et l'application du nouveau mot de passe.
    /// SECURITE : cette page risque de faire l'objet d'une utilisation massive en cas de
    /// tentative de piratage de compte. Contraintes renforcées.
    /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour la personnaliser
    /// Cette dernière est amené à évoluer en fonction des futures mise à jour
    /// </summary>
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<Jeux_OlympiquesUser> _userManager;

        public ResetPasswordModel(UserManager<Jeux_OlympiquesUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Classe interne représentant le modèle d'entrée pour les informations de réinitialisation du mot de passe.
        /// Contraintes renforcées pour la sécurité
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "L'adresse email est requise.")]
            [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide.")]
            [Display(Name = "Adresse email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Le mot de passe est requis.")]
            [StringLength(100, ErrorMessage = "Le {0} doit comporter au moins {2} caractères et au maximum {1} caractères.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nouveau mot de passe")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmer le nouveau mot de passe")]
            [Compare("Password", ErrorMessage = "Le nouveau mot de passe et sa confirmation ne correspondent pas.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Code { get; set; }

        }

        /// <summary>
        /// Gère la requête GET de la page de réinitialisation du mot de passe.
        /// </summary>
        /// <param name="code">Code de réinitialisation du mot de passe encodé</param>
        /// <returns>La page de réinitialisation du mot de passe ou une erreur si le code est manquant</returns>
        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("Un code doit être fourni pour la réinitialisation du mot de passe.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        /// <summary>
        /// Gère la soumission du formulaire de réinitialisation du mot de passe.
        /// </summary>
        /// <returns>Redirige vers la page de confirmation ou affiche les erreurs</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
