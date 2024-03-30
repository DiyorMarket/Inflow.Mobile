using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private  IProductDataStore _productDataStore;
        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<Category> Categories { get; private set; }
        public FilterViewModel(IProductDataStore productDataStore)
        {
            _productDataStore = productDataStore;

            Categories = new ObservableCollection<Category>();
            Products = new ObservableCollection<Product>();
        }
        private decimal _lowestPrice;
        public decimal LowestPrice
        {
            get => _lowestPrice;
            set => SetProperty(ref _lowestPrice, value);
        }

        private decimal _highestPrice;
        public decimal HighestPrice
        {
            get => _highestPrice;
            set => SetProperty(ref _highestPrice, value);
        }

        public async void OnFilter()
        {
            try
            {
                var filteredProducts = await _productDataStore.FilterProducts(new ProductFilters("","",_lowestPrice,_highestPrice,null));

                foreach (var product in filteredProducts)
                {
                    Products.Add(product);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public  async Task LoadElements()
        {
            ApiClient apiService = new ApiClient();
            var categories = await apiService.GetAsync<Category>("categories");

            if (categories.Data.Any())
            {
                foreach (var category in categories.Data)
                {
                    Categories.Add(category);
                }
            }
            return;
        }
    }
}

