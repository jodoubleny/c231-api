using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c231_qrder.Models
{
    public class MenuItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int? Quantity { get; set; }
        public bool? IsCancelled { get; set; }
        public bool? IsOverwritten { get; set; }
    }
}
