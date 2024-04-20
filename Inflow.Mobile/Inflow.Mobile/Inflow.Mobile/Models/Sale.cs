using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow.Mobile.Models
{
    public class Sale
    {
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set;}
        public Customer Customer { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }
}
