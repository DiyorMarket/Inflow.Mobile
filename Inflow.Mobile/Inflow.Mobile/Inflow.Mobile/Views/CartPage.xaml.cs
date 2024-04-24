using Inflow.Mobile.ViewModels.Inflow.Mobile.ViewModels;

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