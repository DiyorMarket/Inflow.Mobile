using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.DataStores.SaleItems;
using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;
using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            var apiClient = new ApiClient();

            var sale = new SaleDataStore(apiClient);

            var saleitem = new SaleItemDataStore(apiClient);

            BindingContext = new HistoryViewModel(sale, saleitem);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as HistoryViewModel)?.LoadSaleHistory();
        }
        private async void OnDetailsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}