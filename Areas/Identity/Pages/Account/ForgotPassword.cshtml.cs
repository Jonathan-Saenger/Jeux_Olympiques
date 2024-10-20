﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Jeux_Olympiques.Areas.Identity.Data;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account
{

    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Jeux_OlympiquesUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<Jeux_OlympiquesUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        /// <summary>
        /// Méthode appelée lors de la soumission du formulaire. Si les données sont valides, elle tente de trouver 
        /// l'utilisateur correspondant à l'adresse email fournie et, si l'email est confirmé, génère un jeton de 
        /// réinitialisation de mot de passe. Ensuite, elle envoie un email via l'API de Sendgrid à l'utilisateur 
        /// contenant un lien pour réinitialiser son mot de passe. 
        /// SECURITE : Redirige vers la page de confirmation si l'email existe et est confirmé, sans révéler les informations 
        /// de l'utilisateur. Si l'email n'existe pas ou n'est pas confirmé, la même redirection est effectuée 
        /// pour ne pas compromettre la sécurité.
        /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour la personnaliser.
        /// Cette dernière est amené à évoluer en fonction des futures mise à jour
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "[JEUX OLYMPIQUES] Changement de mot de passe",
                    $"Bonjour, <br><br> Pour renouveller votre mot de passe, veuillez vous rendre sur ce lien : <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Cliquez ici</a>. <br><br> A très vite sur le site des Jeux Olympiques !");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
