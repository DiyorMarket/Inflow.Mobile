using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inflow.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private IProductDataStore _productDataStore;

        public ObservableCollection<TopFilter> TopFilters { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; private set; }

        private string _searchString = string.Empty;
        public string SearchString
        {
            get => _searchString;
            set
            {
                SetProperty(ref _searchString, value);
                OnApplyFilters();
            }
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

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public ProductFilters Filters
        {
            get
            {
                return new ProductFilters(_searchString, "", _lowestPrice, _highestPrice, null);
            }
        }

        public ICommand ApplyFiltersCommand { get; }

        public HomeViewModel(IProductDataStore productDataStore)
        {
            _productDataStore = productDataStore;
            Title = "Home";

            TopFilters = new ObservableCollection<TopFilter>()
            {
                new TopFilter(1, "All"),
                new TopFilter(2, "Sales"),
                new TopFilter(3, "Popular"),
                new TopFilter(4, "Recommended"),
            };
            Products = new ObservableCollection<Product>();
            Categories = new ObservableCollection<Category>();

            ApplyFiltersCommand = new AsyncCommand(OnApplyFilters);
        }

        public async Task LoadData()
        {
            Products.Clear();
            IsBusy = true;

            try
            {
                Products.Clear();
                var products = await _productDataStore.GetProductsAsync();
                foreach (var product in products)
                {
                    Products.Add(product);
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

        public async Task LoadMoreData()
        {
            IsBusy = true;

            try
            {
                var products = await _productDataStore.GetNextPageAsync(Filters);
                foreach (var product in products)
                {
                    Products.Add(product);
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

        public async Task OnApplyFilters()
        {
            IsBusy = true;
            Products.Clear();
            try
            {
                var filteredProducts = await _productDataStore.FilterProducts(Filters);

                foreach (var product in filteredProducts)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task LoadElements()
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
