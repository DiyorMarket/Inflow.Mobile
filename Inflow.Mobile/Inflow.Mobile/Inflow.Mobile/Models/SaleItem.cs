namespace Inflow.Mobile.Models
{
    public class SaleItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalDue { get; set; }
        public Product Product { get; set; }
    }
}
