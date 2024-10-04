namespace Jeux_Olympiques.Models

{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Offer? Offer { get; set; }   
        public User? User { get; set; } 

    }
}
