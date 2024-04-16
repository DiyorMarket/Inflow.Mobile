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

			BindingContext = _viewModel = new CartViewModel ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.AddProductsToCart();

            CartListView.ItemsSource = null;
            CartListView.ItemsSource = _viewModel.CartItems; // Force ListView to rebind and refresh
        }

    }
}