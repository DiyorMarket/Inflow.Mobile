using System;
using System.Collections.Generic;

namespace Inflow.Mobile.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime SaleDate { get; set; }

        public List<SaleItem> SaleItems { get; set; }

        public Sale()
        {
            SaleItems = new List<SaleItem>();
        }
    }

    public partial class Sale : ObservableModelBase
    {
        private bool _buttomVisible = true;
        public bool ButtomVisible
        {
            get => _buttomVisible;
            set => SetProperty(ref _buttomVisible, value);
        }
        private bool _salesVisible;
        public bool SalesVisible 
        { 
            get => _salesVisible; 
            set => SetProperty(ref _salesVisible, value); 
        }
    }
}
