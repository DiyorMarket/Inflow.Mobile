﻿using Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationPopupPage : PopupPage
    {
        public ConfirmationPopupPage()
        {
            InitializeComponent();
            BindingContext = new ConfirmationViewModel();
        }
    }
}