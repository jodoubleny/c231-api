using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c231_qrder.Services
{
    public class ServiceBase
    {
        public const string restaurantSortKeyPrefix = "INFO#";
        public const string tableSortKeyPrefix = "TABLE#";
        public const string orderSortKeyPrefix = "ORDER#";

        public string GetGuidAsStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

    }
}
