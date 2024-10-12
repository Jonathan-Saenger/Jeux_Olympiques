using Jeux_Olympiques.Areas.Identity.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Jeux_Olympiques.Models
{/// <summary>
/// Les propriétés de ce User concerne l'Administrateur de l'application avec la possibilité de créer des évènements, des offres
/// </summary>
    public class User: Jeux_OlympiquesUser
    {
        public string? Name { get; set; }
        public List<Ticket>? BuyTicket { get; set; }
        public List<Event>? PublishEvent { get; set; }
        public List<Offer>? CreateOffer { get; set; }
        public List<Cart>? ShoppingCart { get; set; }

    }
}
