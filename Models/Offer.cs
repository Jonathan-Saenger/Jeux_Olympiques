using System.ComponentModel.DataAnnotations;

namespace Jeux_Olympiques.Models
{
    public class Offer
    {
        public int Id { get; set; }

        [Display(Name = "Titre")]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public string? Place {  get; set; }

        [Display(Name = "Prix")]
        public float? Price { get; set; }
        public User? Publish { get; set; }
        public List<Ticket>? IsContained { get; set; }

    }

}
