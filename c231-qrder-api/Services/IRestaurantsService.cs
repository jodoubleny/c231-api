using c231_qrder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace c231_qrder.Services
{
    interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDto>> GetAllAsync();
        Task<RestaurantDto> GetByIdAsync(string id);
        Task<RestaurantDto> AddAsync(RestaurantCreateDto restaurantCreateDto);
        Task SaveAsync(RestaurantDto restaurantDto);
        Task RemoveAsync(string id);
        Task<bool> IsRestaurantPresent(string id);
    }
}
