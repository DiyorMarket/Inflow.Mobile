using Inflow.Mobile.DataStores.Customers;
using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class ConfirmationToBuyInCartViewModel : BaseViewModel
    {
        private ISaleDataStore _saleDataStore;
        private ICustomerDataStore _customerDataStore;
        private readonly LoginService _loginService;
        public ICommand SuccesCommand { get; }
        public ICommand CancelCommand { get; }
        public ObservableCollection<Product> ProductsInCart { get; set; }

        public ConfirmationToBuyInCartViewModel(ISaleDataStore saleDataStore, ICustomerDataStore customerDataStore)
        {
            _saleDataStore = saleDataStore;
            _customerDataStore = customerDataStore;
            _loginService = new LoginService();
            SuccesCommand = new Command(async () => await Success());
            CancelCommand = new Command(async () => await Cancel());
            ProductsInCart = new ObservableCollection<Product>();
        }

        private async Task Success()
        {
            await PopupNavigation.Instance.PopAsync();
            CreateSale();
        }

        private void LoadData() 
        {
            var products = DataService.GetProducts("ProductsInCart");

            foreach (var product in products)
            {
                ProductsInCart.Add(product);
            }
        }

        public async void CreateSale()
        {
            var saleItems = new List<SaleItem>();

            var userId = _loginService.GetUserData().Result.UserId;

            var customers = await _customerDataStore.GetCustomersAsync(userId);

            var customer = customers.FirstOrDefault(x => x.UserId == userId);

            LoadData();

            foreach (var item in ProductsInCart)
            {
                if(item.QuantityInStock != 0)
                {
                    saleItems.Add(new SaleItem()
                    {
                        Quantity = item.Quantity,
                        ProductId = item.Id,
                        UnitPrice = item.SalePrice
                    });
                }
            }

            var sale = new Sale()
            {
                SaleDate = DateTime.Now,
                CustomerId = customer.Id,
                SaleItems = saleItems
            };

            var newSale = _saleDataStore.CreateSale(sale);

            if (newSale != null)
            {
                ProductsInCart.Clear();
                DataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");
                await PopupNavigation.Instance.PushAsync(new PurchaseConfirmationPopupPage());
            }
        }


        private async Task Cancel()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to close the modal: " + ex.Message);
            }
        }
    }
}
