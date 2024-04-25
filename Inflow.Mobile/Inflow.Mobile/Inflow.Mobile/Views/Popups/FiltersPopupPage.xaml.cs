using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltersPopupPage : PopupPage
    {
        public FiltersPopupPage()
        {
            InitializeComponent();
            var apiClient = new ApiClient();
            var productDataStore = new ProductDataStore(apiClient);
            BindingContext = new HomeViewModel(productDataStore);
        }

        protected override async void OnAppearing()
        {
            var vm = BindingContext as HomeViewModel;
            await vm?.LoadCategories();
            base.OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as HomeViewModel;
            await vm?.OnApplyFilters();
            await PopupNavigation.Instance.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation?.PopModalAsync();
        }
    }
}