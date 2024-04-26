using Inflow.Mobile.ViewModels;
using System;

namespace Inflow.Mobile.Models
{
    public class Product : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private decimal salePrice;
        public decimal SalePrice
        {
            get => salePrice;
            set
            {
                if (salePrice != value)
                {
                    salePrice = value;
                    OnPropertyChanged(nameof(SalePrice));
                }
            }
        }
        public DateTime ExpireDate { get; set; }
        public int QuantityInStock { get; set; }
        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        private bool _isInCart;

        public bool IsInCart
        {
            get { return _isInCart; }
            set
            {
                SetProperty(ref _isInCart, value);
                OnPropertyChanged(nameof(IsInCart));
            }
        }

        private bool _isSaved;

        public bool IsSaved
        {
            get { return _isSaved; }
            set
            {
                SetProperty(ref _isSaved, value);
                OnPropertyChanged(nameof(IsSaved));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }
    }
}