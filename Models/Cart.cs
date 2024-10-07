using System.ComponentModel.DataAnnotations;

namespace Jeux_Olympiques.Models

{
    /// <summary>
    /// Création du panier
    /// Le visiteur n'a pas besoin d'être connecté pour ajouter des offres dans son panier
    /// </summary>
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Offer? Offer { get; set; }
        public User? User { get; set; }
        public string? CartId { get; internal set; }
        public int OfferId { get; internal set; }
    }
}
