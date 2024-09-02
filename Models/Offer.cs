namespace Jeux_Olympiques.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Place {  get; set; }
        public float? Price { get; set; }
        public User? Publish { get; set; }
        public List<Event>? PresentEvent { get; set; }
        public List<Ticket>? IsContained { get; set; }

    }

}
