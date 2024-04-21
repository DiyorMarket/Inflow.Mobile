﻿using Inflow.Mobile.Models;
using Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseConfirmationPopupPage : PopupPage
    {
        public PurchaseConfirmationPopupPage()
        {
            InitializeComponent();
            BindingContext = new PurchaseConfirmationViewModel();
        }
    }
}