namespace Jeux_Olympiques.Models
{
    public class TicketDetail
    {
        public int TicketDetailId { get; set; }
        public int TicketId { get; set; }
        public int OfferId { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Offer? Offer { get; set; }
        public virtual Ticket? Ticket { get; set; }

    }
}
