using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Inflow.Mobile.ViewModels
{
    namespace Inflow.Mobile.ViewModels
    {
        public class CartViewModel : BaseViewModel
        {
            public ObservableCollection<Product> CartItems { get; set; }
            public ObservableCollection<Product> ProductsInCart { get; set; }
            private Product selectedItem;

            public ICommand IncreaseCommand { get; }
            public ICommand DecreaseCommand { get; }
            public Product SelectedItem
            {
                get => selectedItem;
                set
                {
                    if (selectedItem != value)
                    {
                        if (selectedItem != null) selectedItem.IsSelected = false;
                        selectedItem = value;
                        if (selectedItem != null) selectedItem.IsSelected = true;
                        OnPropertyChanged(nameof(SelectedItem));
                    }
                }
            }

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

                OnPropertyChanged(nameof(CartItems));
            }



            public void RemoveProductFromCart(Product product)
            {
                AddProductsToCart();
                CartItems.Remove(product);
                DataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");
                OnPropertyChanged(nameof(TotalPrice));
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
