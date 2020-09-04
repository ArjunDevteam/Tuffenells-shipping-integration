using AutoMapper;
using Rishvi.Modules.Users.Models.DTOs;

namespace Rishvi.Modules.Users.Models.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            //List
            CreateMap<User, UserAvailableServicesResponseDto>();

            // Create
            //CreateMap<NewUserResponseDto, User>();

            // Edit
            CreateMap<CourierServiceDto, User>();
            //.ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<User, CourierServiceDto>();
                //.ForMember(m => m.Id, opt => opt.Ignore());

            // Edit
            //CreateMap<UserEditDto, User>();
            //.ForMember(m => m.Content, opt => opt.Ignore());

            //CreateMap<User, UserEditDto>();
        }
    }
}