using Inflow.Mobile.DataStores.Customers;
using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels.Inflow.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPage : ContentPage
	{
		CartViewModel _viewModel;
		public CartPage ()
		{
			InitializeComponent ();

            var apiClient = new ApiClient();
            var saleDataStore = new SaleDataStore(apiClient);
            var customerDataStore = new CustomerDataStore(apiClient);

            BindingContext = _viewModel = new CartViewModel (saleDataStore, customerDataStore);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.AddProductsToCart();

            CartListView.ItemsSource = null;
            CartListView.ItemsSource = _viewModel.CartItems; 
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    var vm = BindingContext as CartViewModel;
        //    if (vm != null)
        //    {
        //        vm.SaveProductsAsync();
        //    }
        //}
    }
}