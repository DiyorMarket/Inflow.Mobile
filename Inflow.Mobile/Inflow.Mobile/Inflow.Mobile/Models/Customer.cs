using System;
using System.Collections.Generic;
using System.Text;

namespace Inflow.Mobile.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
