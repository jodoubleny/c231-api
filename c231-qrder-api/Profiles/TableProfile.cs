using AutoMapper;
using c231_qrder.Models;

namespace c231_qrder.Profiles
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<TableCreateDto, Table>();
            CreateMap<Table, TableDto>()
                .ReverseMap();
            CreateMap<Table, TableOrderDto>();
        }
    }
}
