using Jeux_Olympiques.Areas.Identity.Data;

namespace Jeux_Olympiques.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string? Name { get; set; }
        public string? QrCode { get; set; }
        public decimal? Price { get; set; }
        public string? TicketKey { get; set; }
        public System.DateTime TicketDate { get; set; }
        public Jeux_OlympiquesUser? Buyer { get; set; }
        public Offer? Contains { get; set; }
        public List<TicketDetail>? TicketDetails { get; set; }
    }
}
