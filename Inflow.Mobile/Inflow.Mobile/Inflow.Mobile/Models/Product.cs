using System;

namespace Inflow.Mobile.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime ExpireDate { get; set; }
        public int QuantityInStock { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
    }
}