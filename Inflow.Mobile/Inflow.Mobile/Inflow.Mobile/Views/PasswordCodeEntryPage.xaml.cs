﻿using Inflow.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordCodeEntryPage : ContentPage
    {
        public PasswordCodeEntryPage()
        {
            InitializeComponent();
            // this.BindingContext = new PasswordResetViewModel();
        }
    }
}