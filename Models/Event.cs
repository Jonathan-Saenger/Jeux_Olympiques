namespace Jeux_Olympiques.Models
{
    public class Event
    {
        public int Id {  get; set; }
        public string? Entitled { get; set; }
        public string? Presentation { get; set; }
        public DateTime Date { get; set; }
        public string? Site {  get; set; }
        public Offer? Offer { get; set; }
        public User? Created { get; set; }
    }
}
