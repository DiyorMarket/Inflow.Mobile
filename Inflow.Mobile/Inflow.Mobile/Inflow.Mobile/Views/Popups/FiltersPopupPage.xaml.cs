using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltersPopupPage : PopupPage
    {
        private readonly IProductDataStore _productDataStore;
        public FiltersPopupPage()
        {
            InitializeComponent();
            BindingContext = new FilterViewModel(_productDataStore);   
        }

        protected override void OnAppearing()
        {
            var vm = BindingContext as FilterViewModel;
            vm?.LoadElements();
            base.OnAppearing();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var vm=BindingContext as FilterViewModel;
            vm?.OnFilter();

            await Navigation.PopModalAsync();
        }
    }
}