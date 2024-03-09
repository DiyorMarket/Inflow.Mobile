using System;

namespace Inflow.Mobile.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal SalePrice { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public int QuantityInStock { get; private set; }
        public Category Category { get; private set; }
    }
}