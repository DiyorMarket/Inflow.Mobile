using Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
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
            await Navigation.PopModalAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation?.PopModalAsync();
        }
    }
}