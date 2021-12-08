using AutoMapper;
using c231_qrder.Models;

namespace c231_qrder.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ForMember(
                    dest => dest.OrderId,
                    opt => opt.MapFrom(src =>
                        src.TableGuid + src.OrderGuid));
        }
    }
}
