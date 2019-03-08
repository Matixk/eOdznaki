using AutoMapper;
using eOdznaki.Dtos;
using eOdznaki.Models;

namespace eOdznaki.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserForViewDto>();
        }
    }
}