using AutoMapper;
using eOdznaki.Dtos.Announcements;
using eOdznaki.Dtos.ForumPosts;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Dtos.Users;
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
                    dto => dto.MapFrom(e => e.Author.UserName))
                .ReverseMap();

            CreateMap<ForumPost, ForumPostPreviewDto>()
                .ForMember(e => e.AuthorName,
                    dto => dto.MapFrom(e => e.Author.UserName))
                .ReverseMap();

            CreateMap<Announcement, AnnouncementPreviewDto>()
                .ForMember(e => e.AuthorName,
                    dto => dto.MapFrom(e => e.Author.UserName))
                .ReverseMap();
        }
    }
}