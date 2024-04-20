using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private ISaleDataStore _saleDataStore;

        public ObservableCollection<Sale> HistorySale { get; set; }
        public ObservableCollection<SaleItem> HistorySaleItems { get; set; }

        public HistoryViewModel(ISaleDataStore saleDataStore)
        {
            Title = "History";

            _saleDataStore = saleDataStore;
            HistorySale = new ObservableCollection<Sale>();
            HistorySaleItems = new ObservableCollection<SaleItem>();
        }
        public async Task LoadSaleHistory()
        {
            if(IsBusy) return;

            HistorySale.Clear();
            IsBusy = true;

            try
            {
                var sales = await _saleDataStore.GetSales(4);

                foreach(var sale in sales)
                {
                    HistorySale.Add(sale);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        } 

    }
}
