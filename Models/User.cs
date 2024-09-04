using Jeux_Olympiques.Areas.Identity.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Jeux_Olympiques.Models
{
    public class User: Jeux_OlympiquesUser
    {
        public string? Name { get; set; }
        public List<Ticket>? BuyTicket { get; set; }
        public List<Event>? PublishEvent { get; set; }
        public List<Offer>? CreateOffer { get; set; }

    }
}
