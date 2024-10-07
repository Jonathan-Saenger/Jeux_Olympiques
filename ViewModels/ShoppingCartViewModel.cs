using System.Collections.Generic;
using Jeux_Olympiques.Models;

namespace Jeux_Olympiques.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart>? CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}