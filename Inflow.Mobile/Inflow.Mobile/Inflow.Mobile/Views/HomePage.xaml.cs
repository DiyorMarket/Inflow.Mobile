using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;
using Inflow.Mobile.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
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

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            var vm = BindingContext as HomeViewModel;

            if (vm is null)
            {
                return;
            }

            var popup = new FiltersPopupPage()
            {
                BindingContext = vm
            };
            await PopupNavigation.Instance.PushAsync(popup);
        }
    }
}