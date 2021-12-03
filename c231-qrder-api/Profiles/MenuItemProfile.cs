using AutoMapper;
using c231_qrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c231_qrder.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItemCreateDto, MenuItem>();
            CreateMap<MenuItem, MenuItemDto>()
                .ReverseMap();
        }
    }
}
