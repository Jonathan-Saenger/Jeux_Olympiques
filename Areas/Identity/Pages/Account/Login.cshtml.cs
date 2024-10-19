// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Jeux_Olympiques.Areas.Identity.Data;
using Jeux_Olympiques.Models;
using Jeux_Olympiques.Data;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Jeux_OlympiquesUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private ApplicationDbContext _context;

        /// <summary>
        /// Constructeur de la classe LoginModel qui gère la connexion de l'utilisateur.
        /// SECURITE : des contraintes de saisies côté client ont été imposé
        /// ATTENTION : il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour personnaliser
        /// le login de l'utilisateur. Cette dernière est amené à évoluer en fonction des futures mise à jour
        /// </summary>
        /// <param name="signInManager">Gestionnaire de connexion</param>
        /// <param name="logger">Service de journalisation</param>
        /// <param name="context">Contexte de la base de données de l'application</param>
        public LoginModel(SignInManager<Jeux_OlympiquesUser> signInManager, ILogger<LoginModel> logger, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Se souvenir de moi")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
        /// <summary>
        /// Récupération du panier dans le compte de l'utilisateur connecté
        /// </summary>
        /// <param name="userName">Nom d'utilisateur (email) de l'utilisateur connecté</param>
        private void MigrateShoppingCart(string userName)
        {
            var cart = ShoppingCart.GetCart(HttpContext, _context);
            cart.MigrateCart(userName);
            HttpContext.Session.SetString(ShoppingCart.CartSessionKey, userName);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    MigrateShoppingCart(Input.Email); // Récupération du panier sur le compte de l'utilisateur.
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
