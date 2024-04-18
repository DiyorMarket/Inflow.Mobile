using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels.Inflow.Mobile.ViewModels;
using Inflow.Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            (MainPage.BindingContext as CartViewModel)?.SaveProductsAsync();
        }

        protected override void OnResume()
        {
        }
    }
}
