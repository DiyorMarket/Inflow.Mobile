using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Inflow.Mobile.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        private IDataStore<Product> _dataStore;
        public ObservableCollection<TopFilter> TopFilters { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public HomeViewModel()
        {
            Title = "Home";
            TopFilters = new ObservableCollection<TopFilter>()
            {
                new TopFilter(1, "All"),
                new TopFilter(2, "Sales"),
                new TopFilter(3, "Popular"),
                new TopFilter(4, "Recommended"),
            };

            Products = new ObservableCollection<Product>();

            _dataStore = new ProductDataStore();

            LoadData();
        }

        public async Task LoadData()
        {
            var products = await _dataStore.GetItemsAsync();

            foreach (var product in products)
            {
                Products.Add(product);
            }
        }
    }
}
