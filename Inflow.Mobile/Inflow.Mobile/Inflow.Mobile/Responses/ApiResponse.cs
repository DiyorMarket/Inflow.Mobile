using DiyorMarket.Helper;
using Inflow.Mobile.Helpers;
using System.Collections.Generic;

namespace Inflow.Mobile.Responses
{
    public class ApiResponse<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public List<ResourceLink> Links { get; private set; }
        public Metadata Metadata { get; private set; }

        public ApiResponse()
        {
            Data = new List<T>();
            Links = new List<ResourceLink>();
            Metadata = new Metadata();
        }
    }
}
