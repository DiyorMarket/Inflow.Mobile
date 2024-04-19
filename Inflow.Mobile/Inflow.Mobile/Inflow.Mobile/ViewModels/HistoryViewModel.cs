using Inflow.Mobile.DataStores.SaleItems;
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
        private ISaleItemDataStore _saleItemDataStore;
        public ObservableCollection<Sale> SaleHistory { get; set; }
        public ObservableCollection<SaleItem> SaleItems { get; set; }
        public ICommand ShowProductsCommand { get; }

        public HistoryViewModel(ISaleDataStore saleDataStore, ISaleItemDataStore saleItemDataStore)
        {
            Title = "История";

            _saleDataStore = saleDataStore;
            _saleItemDataStore = saleItemDataStore;

            SaleHistory = new ObservableCollection<Sale>();
            SaleItems = new ObservableCollection<SaleItem>();
            ShowProductsCommand = new Command(ShowProducts);
        }

        private void ShowProducts()
        {
            if(IsListViewVisible)
            {
                return;
            }
            IsListViewVisible = true;
            LoadSaleItems();
        }
        private async Task LoadSaleItems()
        {
            IsBusy = true;

            try
            {
                SaleItems.Clear();
                var saleItems = await _saleItemDataStore.GetSaleItems(15);
                foreach (var item in saleItems)
                {
                    item.ProductImage += item.Product.ImageUrl;
                    SaleItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sale items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
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
    }
}
