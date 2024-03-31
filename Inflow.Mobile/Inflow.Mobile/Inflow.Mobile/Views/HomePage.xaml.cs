using System;
using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;
using Inflow.Mobile.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private bool isYellow = false;

        public HomePage()
        {
            InitializeComponent(); 

            var apiClient = new ApiClient();
            var productDataStore = new ProductDataStore(apiClient);
            BindingContext = new HomeViewModel(productDataStore);
        }

        protected override async void OnAppearing()
        {
            var vm = BindingContext as HomeViewModel;
            await vm?.LoadData();

            base.OnAppearing();
        }

        private async void ItemsView_OnRemainingItemsThresholdReached(object sender, EventArgs e)
        {
            var vm = BindingContext as HomeViewModel;
            await vm?.LoadMoreData();
        }

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            var popup = new FiltersPopupPage();
            await Navigation.PushModalAsync(popup);
        }

        public async void Add_To_Cart(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;

            if(isYellow)
            {
                button.Source = "grey_cart.png";
                isYellow = false;
            }
            else
            {
                button.Source = "blue_cart.png";
                isYellow = true;
            }

            await button.ScaleTo(1.2, 100, Easing.Linear);
            await button.ScaleTo(1, 100, Easing.Linear);
        }
    }
}