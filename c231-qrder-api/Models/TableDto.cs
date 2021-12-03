using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c231_qrder.Models
{
    public class TableDto
    {
        public string RestaurantId { get; set; }
        public string TableId { get; set; }
        public string TableName { get; set; }
        public Order? OccupiedOrder { get; set; }
    }
}
