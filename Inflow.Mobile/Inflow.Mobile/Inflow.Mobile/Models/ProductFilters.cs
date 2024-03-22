namespace Inflow.Mobile.Models
{
    public class ProductFilters
    {
        public string SearchString { get; set; }
        public string Sort { get; set; }
        public decimal? LowestPrice { get; set; }
        public decimal? HighestPrice { get; set; }
        public int? CategoryId { get; set; }

        public ProductFilters(string searchString, string sort)
        {
            SearchString = searchString;
            Sort = sort;
        }

        public ProductFilters(string searchString, string sort, decimal? lowestPrice, decimal? highestPrice, int? categoryId)
            : this(searchString, sort)
        {
            LowestPrice = lowestPrice;
            HighestPrice = highestPrice;
            CategoryId = categoryId;
        }
    }
}
