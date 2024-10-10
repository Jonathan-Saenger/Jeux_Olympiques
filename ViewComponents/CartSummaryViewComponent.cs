using Microsoft.AspNetCore.Mvc;
using Jeux_Olympiques.Models;
using Jeux_Olympiques.ViewModels;

namespace Jeux_Olympiques.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public CartSummaryViewComponent(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetCartItems();

            var viewModel = new CartSummaryViewModel
            {
                CartCount = items.Sum(x => x.Quantity),
                CartTotal = (decimal)items.Sum(x => x.Quantity * x.Offer.Price)
            };

            return View(viewModel);
        }
    }
}