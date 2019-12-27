using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.DTO;
using WEB.Models;
namespace WEB.Mapping
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Chat, ChatDTO>().ReverseMap();
        }
    }
}
