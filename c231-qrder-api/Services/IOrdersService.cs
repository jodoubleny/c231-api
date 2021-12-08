using c231_qrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c231_qrder.Services
{
    interface IOrdersService
    {
        Task<IEnumerable<OrderDto>> GetAllByRestaurantIdAsync(string id);
        Task AddAsync(string id, OrderCreateDto orderCreateDto);
        Task SaveAsync(string id, OrderDto orderDto);
        Task RemoveAsync(string id, string orderId);
        Task<bool> IsRestaurantAvailable(string id);
        Task<bool> IsOrderPresent(string id, string orderId);
    }
}
