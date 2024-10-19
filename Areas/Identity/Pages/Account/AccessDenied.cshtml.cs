// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jeux_Olympiques.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Renvoi un message d'erreur si un utilisateur n'est pas autorisé à accéder à un espace dédié
    /// ATTENTION : Il s'agit d'une API par défaut d'ASP.NET CORE Identity que nous avons repris pour personnaliser
    /// un message d'accès. Cette dernière est amené à évoluer en fonction des futures mise à jour
    /// </summary>
    public class AccessDeniedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
