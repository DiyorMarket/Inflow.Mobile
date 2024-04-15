using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow.Mobile.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal totalDue { get; set; }
        public int Quantity { get; set; }

        public List<SaleItem> SaleItems { get; set; }

        public Sale()
        {
            SaleItems = new List<SaleItem>();
        }
    }
}
