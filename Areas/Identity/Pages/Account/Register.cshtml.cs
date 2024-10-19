// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Jeux_Olympiques.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Jeux_Olympiques.Models;
using Jeux_Olympiques.Data;   

namespace Jeux_Olympiques.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Modèle de page pour l'inscription des utilisateurs dans l'application. 
    /// Cette classe gère le processus d'inscription, y compris la validation des informations saisies,
    /// la création du compte utilisateur, la génération d'une clé unique, la migration du panier d'achat,
    /// et l'envoi d'un email de confirmation.
    /// SECURITE : PAGE CRITIQUE SECURITE. Les contraintes de validation sur les champs de saisis ont été
    /// renforcé. 
    /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour personnaliser
    /// toute l'inscription de l'utilisateur. Cette dernière est amenée à évoluer en fonction des futures mise à jour
    /// </summary>
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Jeux_OlympiquesUser> _signInManager;
        private readonly UserManager<Jeux_OlympiquesUser> _userManager;
        private readonly IUserStore<Jeux_OlympiquesUser> _userStore;
        private readonly IUserEmailStore<Jeux_OlympiquesUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<Jeux_OlympiquesUser> userManager,
            IUserStore<Jeux_OlympiquesUser> userStore,
            SignInManager<Jeux_OlympiquesUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// Modèle d'entrée pour les informations d'inscription de l'utilisateur. SECURITE renforcée avec des
        /// contraintes de validation importantes. 
        /// </summary>
        public class InputModel
        {
            //Ajouts personnalisés
            [Required(ErrorMessage = "Le prénom est requis.")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[A-ZÀ-ÿ]+[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Veuillez utiliser uniquement des lettres")]
            [StringLength(100, ErrorMessage = "Le {0} doit comporter au moins {2} caractères et au maximum {1} caractères.", MinimumLength = 1)]
            [Display(Name = "Prénom")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Le nom est requis.")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[A-ZÀ-ÿ]+[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Veuillez utiliser uniquement des lettres")]
            [StringLength(100, ErrorMessage = "Le {0} doit comporter au moins {2} caractères et au maximum {1} caractères.", MinimumLength = 1)]
            [Display(Name = "Nom")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "L'adresse email est requise.")]
            [EmailAddress(ErrorMessage = "L'adresse email saisie n'est pas valide.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Le mot de passe est requis.")]
            [StringLength(100, ErrorMessage = "Le {0} doit comporter au moins {2} caractères et au maximum {1} caractères.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmer le mot de passe")]
            [Compare("Password", ErrorMessage = "Les deux mots de passe doivent être identiques")]
            public string ConfirmPassword { get; set; }
        }
        
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        /// <summary>
        /// Migre le panier d'achat de la session vers le compte utilisateur nouvellement créé.
        /// </summary>
        /// <param name="userName">Nom d'utilisateur (email) du nouvel utilisateur</param>
        private void MigrateShoppingCart(string userName)
        {
            var cart = ShoppingCart.GetCart(HttpContext, _context);
            cart.MigrateCart(userName);
            HttpContext.Session.SetString(ShoppingCart.CartSessionKey, userName);
        }

        /// <summary>
        /// Gère la soumission du formulaire d'inscription.
        /// Crée un nouvel utilisateur, génère une clé unique, migre le panier d'achat,
        /// et envoie un email de confirmation.
        /// </summary>
        /// <param name="returnUrl">URL de retour après inscription</param>
        /// <returns>Résultat de l'action d'inscription</returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName; 

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
   
                user.GenerateAccountKey(); //Création de la clé unique utilisateur

                var result = await _userManager.CreateAsync(user, Input.Password);

                MigrateShoppingCart(Input.Email); // Récupération du panier sur le compte de l'utilisateur.

                if (result.Succeeded)
                {
                    _logger.LogInformation("L'utilisteur a créé un nouveau compte avec mot de passe.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "[JEUX OLYMPIQUES] Validation de votre inscription",
                        $"Bonjour, <br><br> Merci pour votre inscription! Pour activer votre compte, veuillez vous rendre sur ce lien : <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquez ici</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        /// <summary>
        /// Crée une nouvelle instance de l'utilisateur Jeux_OlympiquesUser.
        /// </summary>
        /// <returns>Une nouvelle instance de Jeux_OlympiquesUser</returns>
        /// <exception cref="InvalidOperationException">Levée si la création de l'instance échoue</exception>
        private Jeux_OlympiquesUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Jeux_OlympiquesUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Jeux_OlympiquesUser)}'. " +
                    $"Ensure that '{nameof(Jeux_OlympiquesUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Jeux_OlympiquesUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Jeux_OlympiquesUser>)_userStore;
        }
    }
}
