namespace Jeux_Olympiques.Models

{
    /// <summary>
    /// Création du panier
    /// Le visiteur n'a pas besoin d'être connecté pour ajouter des offres dans son panier
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Identifiant unique du panier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quantité d'articles dans le panier.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Prix total du panier en fonction de la quantité et de l'offre.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Date et heure de création du panier.
        /// </summary>
        public System.DateTime DateCreated { get; set; }

        /// <summary>
        /// Offre associée au panier, qui peut être null si aucune offre n'est appliquée.
        /// </summary>
        public Offer? Offer { get; set; }

        /// <summary>
        /// Utilisateur propriétaire du panier, qui peut être null s'il n'est pas encore attribué à un utilisateur.
        /// </summary>
        public User? User { get; set; }
    }
}
