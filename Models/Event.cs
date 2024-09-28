using System.ComponentModel.DataAnnotations;

namespace Jeux_Olympiques.Models
{
    public enum Sport
    {
        Athéltisme,
        Haltérophilie,
        Natation,
        Escrime,
        Equitation,
        Football,
        Basketball,
        [Display(Name = "Tennis de table")]
        TennisDeTable,
        [Display(Name = "Volley ball")]
        VolleyBall,

    }
    public enum Lieu
    {
        [Display(Name = "Paris - Parc des Princes")]
        ParcDesPrinces,
        [Display(Name = "Paris - Stade Rolland Garros")]
        StadeRollandGarros,
        [Display(Name="Saint-Denis - Athlétisme Stade de France")]
        AthetismeStadeDeFrance,
        [Display(Name = "Strasbourg - Gymnase Rhénus")]
        GymnaseRhénus,
        [Display(Name = "Angers - Centre Aquatique Olympique")]
        CentreAquatiqueOlympique,
    }


    public class Event
    {
        public int Id {  get; set; }

        [Display(Name = "Sport")]
        public Sport Entitled { get; set; }

        public string? Presentation { get; set; }

        [Display(Name = "Date et heure")]
        public DateTime? Date { get; set; }

        [Display(Name = "Lieu")]
        public Lieu Site { get; set; }

        [Display(Name = "Choix du ticket")]
        public User? Created { get; set; }

        public ICollection<Offer>? Offers { get; set; }
    }
}
