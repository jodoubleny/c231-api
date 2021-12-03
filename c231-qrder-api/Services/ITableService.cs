using c231_qrder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace c231_qrder.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllByRestaurantIdAsync(string id);
        Task AddAsync(string id, TableCreateDto tableCreateDto);
        //Task SaveAsync(IEnumerable<TableDto> tableDtos);
        //Task RemoveAsync(string id, string tableId);
        Task<bool> IsPresent(string id);
        Task<bool> IsPresent(string id, string tableId);
    }
}
