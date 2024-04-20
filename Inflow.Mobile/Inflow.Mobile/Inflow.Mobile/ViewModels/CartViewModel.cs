using Inflow.Mobile.DataStores.Customers;
using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    namespace Inflow.Mobile.ViewModels
    {
        public class CartViewModel : BaseViewModel
        {
            private  ISaleDataStore _saleDataStore;
            private  ICustomerDataStore _customerDataStore;
            private readonly LoginService _loginService;

            private System.Timers.Timer saveTimer;
            public ObservableCollection<Product> CartItems { get; set; }
            public ObservableCollection<Product> ProductsInCart { get; set; }

            public ICommand RemoveCommand { get; private set; }
            public ICommand IncreaseCommand { get; }
            public ICommand DecreaseCommand { get; }
            public ICommand ShowConfirmationCartCommand { get; }

            private Product selectedItem;
            public ICommand BuyProducts {  get; }
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

            public CartViewModel(ISaleDataStore saleDataStore, ICustomerDataStore customerDataStore)
            {
                _saleDataStore = saleDataStore;
                _customerDataStore = customerDataStore;

                CartItems = new ObservableCollection<Product>();
                CartItems.CollectionChanged += OnCartItemsChanged;
                ProductsInCart = new ObservableCollection<Product>();

                RemoveCommand = new Command<Product>(RemoveProductFromCart);
                IncreaseCommand = new Command<Product>(IncreaseQuantity);
                DecreaseCommand = new Command<Product>(DecreaseQuantity);
                ShowConfirmationCartCommand = new Command(async () => await ShowConfirmationPopup());
                BuyProducts = new Command(CreateSale);
                _loginService = new LoginService();

                saveTimer = new System.Timers.Timer(5000);
                saveTimer.Elapsed += OnSaveTimerElapsed;
                saveTimer.AutoReset = false;

                MessagingCenter.Subscribe<ConfirmationCartViewModel>(this, "CartUpdated", (sender) =>
                {
                    CartItems.Clear();
                    OnPropertyChanged(nameof(TotalPrice));
                });

                AddProductsToCart();
                _customerDataStore = customerDataStore;
            }

            private async Task ShowConfirmationPopup()
            {
                await PopupNavigation.Instance.PushAsync(new ConfirmationCartPopupPage());
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
                    product.IsSelected = false;
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

            private async Task ClearAllProductsInCart()
            {
                ProductsInCart.Clear();
                DataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");
                await PopupNavigation.Instance.PopAsync();
                MessagingCenter.Send(this, "CartUpdated");
            }

            private async void CreateSale()
            {
                if(CartItems == null)
                {
                    return;
                }
                var saleItems = new List<SaleItem>();

                var userId = _loginService.GetUserData().Result.UserId;

                var customers = await _customerDataStore.GetCustomersAsync(userId);

                var customer = customers.FirstOrDefault(x => x.UserId == userId);

                foreach (var item in CartItems)
                {
                    saleItems.Add(new SaleItem()
                    {
                        Quantity = item.Quantity,
                        ProductId = item.Id,
                        UnitPrice = item.SalePrice
                    });
                }

                var sale = new Sale()
                {
                    SaleDate = DateTime.Now,
                    CustomerId = customer.Id,
                    SaleItems = saleItems
                };

                var newSale = _saleDataStore.CreateSale(sale);

                if(newSale == null)
                {
                    // Popup chiqarish kerak.
                }

                ClearAllProductsInCart();
            }
        }
    }
}
