using Api.Models.User;
using AutoMapper;
using Data.Entities;

namespace Api
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            MapUsers();
        }

        private void MapUsers()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))          
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
