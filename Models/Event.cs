using System.ComponentModel.DataAnnotations;

namespace Jeux_Olympiques.Models
{
    public class Event
    {
        public int Id {  get; set; }

        [Display(Name = "Sport")]
        public string? Entitled { get; set; }

        public string? Presentation { get; set; }

        [Display(Name = "Date et heure")]
        public DateTime? Date { get; set; }

        [Display(Name = "Lieu")]
        public string? Site {  get; set; }

        [Display(Name = "Choix du ticket")]
        public User? Created { get; set; }

        public ICollection<Offer>? Offers { get; set; }
    }
}
