using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Inflow.Mobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly ISaleDataStore _saleDataStore;

        public Command<Sale> ShowProductsCommand { get; private set; }

        public ObservableCollection<Sale> SaleHistory { get; private set; }

        public HistoryViewModel(ISaleDataStore saleDataStore)
        {
            Title = "History";

            _saleDataStore = saleDataStore;
            SaleHistory = new ObservableCollection<Sale>();
            ShowProductsCommand = new Command<Sale>(ShowProducts);
        }

        private void ShowProducts(Sale sale)
        {
            sale.SalesVisible = !sale.SalesVisible;
        }

        public async Task LoadSaleHistory()
        {
            if (IsBusy) return;

            SaleHistory.Clear();
            IsBusy = true;

            try
            {
                var sales = await _saleDataStore.GetSales(4);
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
