// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jeux_Olympiques.Areas.Identity.Data;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Modèle de page utilisé pour changer le mot de passe d'un utilisateur depuis son espace.
    /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour personnaliser
    /// l'utilisateur. Cette dernière est amenée à évoluer en fonction des futures mise à jour.
    /// SECURITE : les contraintes validation des champs de saisies côté client ont été renforcé
    /// </summary>
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<Jeux_OlympiquesUser> _userManager;
        private readonly SignInManager<Jeux_OlympiquesUser> _signInManager;

        public SetPasswordModel(
            UserManager<Jeux_OlympiquesUser> userManager,
            SignInManager<Jeux_OlympiquesUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
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

            if (hasPassword)
            {
                return RedirectToPage("./ChangePassword");
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

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Votre mot de passe a été défini.";

            return RedirectToPage();
        }
    }
}
