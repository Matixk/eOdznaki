using AutoMapper;
using eOdznaki.Dtos;
using eOdznaki.Models;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Models;

namespace eOdznaki.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, UserForViewDto>();
            CreateMap<User, UserForPreviewDto>();
            CreateMap<ForumThread, ForumThreadPreviewDto>()
                .ForMember(e => e.AuthorName,
                    dto => dto.MapFrom(e=> e.Author.Id))
                .ReverseMap();

            CreateMap<ForumPost, ForumThreadPreviewDto>()
                .ForMember(e => e.AuthorName,
                    dto => dto.MapFrom(e => e.Author.UserName))
                .ReverseMap();
        }
    }
}