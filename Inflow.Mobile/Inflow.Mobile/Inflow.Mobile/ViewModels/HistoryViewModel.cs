using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private ISaleDataStore _saleDataStore;
        public ObservableCollection<Sale> SaleHistory { get; set; }
        public ObservableCollection<SaleItem> SaleItems { get; set; }
        public ICommand ShowProductsCommand { get; }

        public HistoryViewModel(ISaleDataStore saleDataStore)
        {
            Title = "История";

            _saleDataStore = saleDataStore;
            SaleHistory = new ObservableCollection<Sale>();
            SaleItems = new ObservableCollection<SaleItem>();
            ShowProductsCommand = new Command(ShowProducts);
            IsListViewVisible = false;
        }

        private void ShowProducts()
        {
            IsListViewVisible = !IsListViewVisible;
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
                Console.WriteLine($"Ошибка при загрузке истории продаж: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
