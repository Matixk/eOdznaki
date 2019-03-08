using AutoMapper;
using eOdznaki.Dtos.ForumThreads;
using eOdznaki.Models;

namespace eOdznaki.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
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