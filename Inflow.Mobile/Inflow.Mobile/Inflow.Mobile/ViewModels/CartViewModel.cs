using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Inflow.Mobile.ViewModels
{
    namespace Inflow.Mobile.ViewModels
    {
        public class CartViewModel : BaseViewModel
        {
            public ObservableCollection<Product> CartItems { get; set; }
            public ObservableCollection<Product> ProductsInCart { get; set; }

            public decimal TotalPrice
            {
                get { return CartItems.Sum(item => item.SalePrice); }
            }
            
            public CartViewModel()
            {
                CartItems = new ObservableCollection<Product>();
                ProductsInCart = new ObservableCollection<Product>();
                AddProductsToCart();
            }

            public void AddProductToCart(Product product)
            {
                AddProductsToCart();

                ProductsInCart.Add(product);

                DataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");

                OnPropertyChanged(nameof(TotalPrice)); 
            }

            public void AddProductsToCart()
            {
                var productsInCart = DataService.GetProducts("ProductsInCart");
                ProductsInCart.Clear();
                CartItems.Clear();

                foreach (var product in productsInCart)
                {
                    ProductsInCart.Add(product);
                    CartItems.Add(product);
                }

                OnPropertyChanged(nameof(CartItems)); // Notify that CartItems has changed
            }



            public void RemoveProductFromCart(Product product)
            {
                AddProductsToCart();
                CartItems.Remove(product);
                DataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
    }
}
