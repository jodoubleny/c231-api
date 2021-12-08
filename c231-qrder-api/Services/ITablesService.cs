using c231_qrder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace c231_qrder.Services
{
    public interface ITablesService
    {
        Task<IEnumerable<TableOrderDto>> GetAllByRestaurantIdAsync(string id);
        Task AddAsync(string id, TableCreateDto tableCreateDto);
        Task SaveAsync(string id, TableDto tableDto);
        Task RemoveAsync(string id, string tableId);
        Task<bool> IsRestaurantPresent(string id);
        Task<bool> IsTablePresent(string id, string tableId);
    }
}
