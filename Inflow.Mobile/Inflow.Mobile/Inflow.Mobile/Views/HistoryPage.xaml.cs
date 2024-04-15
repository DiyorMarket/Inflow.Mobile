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
            var api = new ApiClient();
            var saleDataStore = new SaleDataStore(api);

            BindingContext = new HistoryViewModel(saleDataStore);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as HistoryViewModel)?.LoadSaleHistory();
        }
        private async void OnDetailsButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Sale sale)
            {
                // Перейти на новую страницу для отображения деталей продажи
                // Например:
                // await Shell.Current.Navigation.PushAsync(new SaleItemsPage(sale));
            }
        }
    }
}