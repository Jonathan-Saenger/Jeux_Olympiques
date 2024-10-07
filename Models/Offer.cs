using System.ComponentModel.DataAnnotations;

namespace Jeux_Olympiques.Models
{
    public class Offer
    {
        public int OfferId { get; set; }

        [Display(Name = "Titre")]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public string? Place {  get; set; }

        [Display(Name = "Prix")]
        public decimal? Price { get; set; }
        public User? Publish { get; set; }

        [Display(Name = "Evènement")]
        public int EventId { get; set; }

        public Event? Events { get; set; }   
        public List<Ticket>? IsContained { get; set; }
        public List<Cart>? Carts { get; set; }
    }

}
