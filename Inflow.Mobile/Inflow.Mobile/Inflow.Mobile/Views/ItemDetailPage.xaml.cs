﻿using Inflow.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Inflow.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}