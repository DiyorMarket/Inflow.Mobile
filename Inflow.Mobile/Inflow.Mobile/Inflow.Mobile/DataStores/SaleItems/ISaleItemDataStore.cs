using Inflow.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.SaleItems
{
    public interface ISaleItemDataStore
    {
        Task<IEnumerable<SaleItem>> GetSaleItems(int saleId);
    }
}
