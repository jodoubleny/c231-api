using System.Collections.Generic;

namespace c231_qrder.Models
{
    public class OrderCreateDto
    {
        public List<AssignedTableDto> AssignedTables { get; set; }
        public List<MenuItem> OrderedItems { get; set; }
    }
}
