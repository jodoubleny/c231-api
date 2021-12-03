using System.Collections.Generic;

namespace c231_qrder.Models
{
    public class RestaurantDto
    {
        public string RestaurantId { get; set; }
        public string SortKey { get; set; }
        public string RestaurantName { get; set; }
        public bool IsRunning { get; set; }
        public List<MenuItem> Menus { get; set; }
    }
}
