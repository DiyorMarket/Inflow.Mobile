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
            var popup = new FiltersPopupPage(); ;
            await Navigation.PushModalAsync(popup);
        }
    }
}