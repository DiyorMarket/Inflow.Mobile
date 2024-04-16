
using Inflow.Mobile.ViewModels;
using Inflow.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Inflow.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Shell.Current.Navigation.PushAsync(new HomePage());
            base.OnAppearing();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
