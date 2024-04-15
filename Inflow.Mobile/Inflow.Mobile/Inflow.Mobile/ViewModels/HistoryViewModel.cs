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
        private Sale _selectedSale;

        public ObservableCollection<Sale> SaleHistory { get; set; }

        public Command<Sale> ViewSaleItemsCommand { get; }
        public ICommand ExpandSaleItemCommand { get; set; }

        public HistoryViewModel(ISaleDataStore saleDataStore)
        {
            Title = "История";

            _saleDataStore = saleDataStore;
            SaleHistory = new ObservableCollection<Sale>();

            ViewSaleItemsCommand = new Command<Sale>(async (sale) => await ViewSaleItemsAsync(sale));
            ExpandSaleItemCommand = new Command<Sale>(ExpandSaleItem);
        }
        private void ExpandSaleItem(Sale sale)
        {
            
            foreach (var saleItem in sale.SaleItems)
            {
                saleItem.IsExpanded = true;
            }
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

        private async Task ViewSaleItemsAsync(Sale sale)
        {
            // Можно перейти на новую страницу для отображения деталей продажи
            // Например:
            // await Shell.Current.Navigation.PushAsync(new SaleItemsPage(sale));
        }
    }
}
