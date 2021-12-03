using AutoMapper;
using c231_qrder.Models;

namespace c231_qrder.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<RestaurantCreateDto, Restaurant>();
            CreateMap<Restaurant, RestaurantDto>()
                .ReverseMap();
        }
    }
}
