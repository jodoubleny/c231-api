using System.Collections.Generic;

namespace c231_qrder.Models
{
    public class OrderCreateDto
    {
        public string RestaurantId { get; set; }
        public string TableGuid { get; set; }
        public List<MenuItem> OrderedItems { get; set; }
    }
}
