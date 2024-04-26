using Inflow.Mobile.DataStores.Customers;
using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Inflow.Mobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly ISaleDataStore _saleDataStore;
        private readonly ICustomerDataStore _customerDataStore;
        private LoginService _loginService;

        public Command<Sale> ShowProductsCommand { get; private set; }

        public ObservableCollection<Sale> SaleHistory { get; private set; }

        public HistoryViewModel(ISaleDataStore saleDataStore, ICustomerDataStore customerDataStore)
        {
            Title = "History";

            _saleDataStore = saleDataStore;
            _customerDataStore = customerDataStore;
            _loginService = new LoginService();
            SaleHistory = new ObservableCollection<Sale>();
            ShowProductsCommand = new Command<Sale>(ShowProducts);
        }

        private void ShowProducts(Sale sale)
        {
            sale.SalesVisible = !sale.SalesVisible;
            sale.ButtomVisible = !sale.ButtomVisible;
        }

        public async Task LoadSaleHistory()
        {
            if (IsBusy) return;

            SaleHistory.Clear();
            IsBusy = true;

            try
            {
                var userId = _loginService.GetUserData().Result.UserId;

                var customers = await _customerDataStore.GetCustomersAsync(userId);

                var customer = customers.FirstOrDefault(x => x.UserId == userId);

                var sales = await _saleDataStore.GetSales(customer.Id);
                foreach (var sale in sales)
                {
                    sale.Quantity = 0;
                    foreach (var saleItem in sale.SaleItems)
                    {
                        sale.Quantity += saleItem.Quantity;
                    }
                    SaleHistory.Add(sale);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sales history: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
