﻿using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    namespace Inflow.Mobile.ViewModels
    {
        public class CartViewModel : BaseViewModel
        {
            private System.Timers.Timer saveTimer;
            public ObservableCollection<Product> CartItems { get; set; }
            public ObservableCollection<Product> ProductsInCart { get; set; }
            public ICommand RemoveCommand { get; private set; }
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
                get { return CartItems.Sum(item => item.Quantity * item.SalePrice); }
            }

            public CartViewModel()
            {
                CartItems = new ObservableCollection<Product>();
                CartItems.CollectionChanged += OnCartItemsChanged;
                ProductsInCart = new ObservableCollection<Product>();

                RemoveCommand = new Command<Product>(RemoveProductFromCart);
                IncreaseCommand = new Command<Product>(IncreaseQuantity);
                DecreaseCommand = new Command<Product>(DecreaseQuantity);

                saveTimer = new System.Timers.Timer(5000);
                saveTimer.Elapsed += OnSaveTimerElapsed;
                saveTimer.AutoReset = false;

                AddProductsToCart();
            }

            private void OnCartItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.OldItems != null)
                {
                    foreach (Product product in e.OldItems)
                        product.PropertyChanged -= OnProductPropertyChanged;
                }
                if (e.NewItems != null)
                {
                    foreach (Product product in e.NewItems)
                        product.PropertyChanged += OnProductPropertyChanged;
                }

                OnPropertyChanged(nameof(TotalPrice));
            }

            private void UpdateTotalPrice()
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    OnPropertyChanged(nameof(TotalPrice));
                });
            }



            private void OnProductPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(Product.Quantity) || e.PropertyName == nameof(Product.SalePrice))
                {
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }

            public async void SaveProductsAsync()
            {
                DataService.SaveProductsAsync(CartItems, "ProductsInCart");
            }

            private void OnQuantityChanged()
            {
                saveTimer.Stop();
                saveTimer.Start();
            }

            private void OnSaveTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    SaveProductsAsync();
                    saveTimer.Stop();
                });
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
                    if(product.Quantity == 0) product.Quantity = 1;
                    ProductsInCart.Add(product);
                    CartItems.Add(product);
                }

                OnPropertyChanged(nameof(CartItems));
            }



            private void RemoveProductFromCart(Product product)
            {
                if (product != null)
                {
                    CartItems.Remove(product);
                    DataService.SaveProductsAsync(CartItems, "ProductsInCart");
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }

            private void IncreaseQuantity(Product product)
            {
                if (product != null && product.Quantity < product.QuantityInStock)
                {
                    product.Quantity++;
                    OnPropertyChanged(nameof(CartItems));
                    UpdateTotalPrice();
                    OnQuantityChanged();
                }
            }

            private void DecreaseQuantity(Product product)
            {
                if (product != null && product.Quantity > 1)
                {
                    product.Quantity--;
                    OnPropertyChanged(nameof(CartItems));
                    UpdateTotalPrice();
                    OnQuantityChanged();
                }
            }



            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}