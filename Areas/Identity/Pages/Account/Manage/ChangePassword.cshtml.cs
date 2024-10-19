// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Jeux_Olympiques.Areas.Identity.Data;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Modèle de page permettant à un utilisateur de changer son mot de passe.
    /// Ce modèle gère la récupération et la validation des informations liées au mot de passe,
    /// et effectue les opérations de modification à l'aide du gestionnaire des utilisateurs (UserManager).
    /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour personnaliser
    /// le changement de mot de passe. Cette dernière est amené à évoluer en fonction des futures mise à jour
    /// </summary>
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<Jeux_OlympiquesUser> _userManager;
        private readonly SignInManager<Jeux_OlympiquesUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<Jeux_OlympiquesUser> userManager,
            SignInManager<Jeux_OlympiquesUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Classe contenant les champs requis pour le formulaire de changement de mot de passe.
        /// ATTENTION SECURITE : contraintes renforcées pour éviter les piratages de comptes
        /// </summary>
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe actuel")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "le {0} doit contenir au moins {2} caractères et au maximum {1} caractère.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nouveau mot de passe")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmer le nouveau mot de passe")]
            [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et la confirmation ne correspondent pas.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("L'utilisateur a changé son mot de passe avec succès.");
            StatusMessage = "Votre mot de passe a été changé.";

            return RedirectToPage();
        }
    }
}
