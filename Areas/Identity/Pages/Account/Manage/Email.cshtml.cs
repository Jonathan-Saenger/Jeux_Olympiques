// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Jeux_Olympiques.Areas.Identity.Data;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Modèle de page permettant aux utilisateurs de consulter et de mettre à jour leur adresse email.
    /// Gère également l'envoi des liens de confirmation pour la modification et la vérification des emails
    /// via l'API de Sendgrid.
    /// SECURITE sur tous les champs de saisies pour éviter les piratages.
    /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour personnaliser
    /// l'utilisateur. Cette dernière est amené à évoluer en fonction des futures mise à jour
    /// </summary>
    public class EmailModel : PageModel
    {
        private readonly UserManager<Jeux_OlympiquesUser> _userManager;
        private readonly SignInManager<Jeux_OlympiquesUser> _signInManager;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Constructeur du modèle de la page email, rôle des injections : 
        /// </summary>
        /// <param name="userManager">Gestionnaire des utilisateurs ASP.NET Core Identity.</param>
        /// <param name="signInManager">Gestionnaire de la connexion des utilisateurs.</param>
        /// <param name="emailSender">Service d'envoi d'emails.</param>
        public EmailModel(
            UserManager<Jeux_OlympiquesUser> userManager,
            SignInManager<Jeux_OlympiquesUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Nouvelle adresse email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(Jeux_OlympiquesUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        /// <summary>
        /// Gère la soumission du formulaire pour changer l'adresse email de l'utilisateur.
        /// Si la nouvelle adresse email est différente de l'ancienne, un email de confirmation est envoyé
        /// via l'API de Sendgrid
        /// </summary>
        /// <returns>Redirige vers la page actuelle avec un message d'état après soumission.</returns>
        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Confirmez votre adresse email",
                    $"Veuillez confirmer votre compte en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant ici</a>.");

                StatusMessage = "Le lien de confirmation pour changer l'email a été envoyé. Veuillez vérifier votre email.";
                return RedirectToPage();
            }

            StatusMessage = "Votre email n'a pas été modifié.";
            return RedirectToPage();
        }

        /// <summary>
        /// Gère la soumission du formulaire pour renvoyer un email de vérification.
        /// </summary>
        /// <returns>Redirige vers la page actuelle après l'envoi de l'email de vérification.</returns>
        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirmez votre adresse email",
                $"Veuillez confirmer votre compte en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant ici</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
