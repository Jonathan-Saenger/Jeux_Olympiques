namespace Jeux_Olympiques.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? QrCode { get; set; }
        public float Price { get; set; }
        public string? TicketKey { get; set; }
        public User? Buyer  { get; set; }
    }
}
