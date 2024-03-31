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

