using AutoMapper;
using News.Api.Application.Stories.v1;
using News.Api.Infrastructure.Services.HackerNews.v0.Models;

namespace News.Api.Application.MappingProfiles;

public class StoryResponse_GetBestStoriesResponse_Profile : Profile
{
    public StoryResponse_GetBestStoriesResponse_Profile() =>
        CreateMap<StoryResponse, GetBestStoriesResponse>()
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.Uri, opt => opt.MapFrom(s => s.Url))
            .ForMember(d => d.PostedBy, opt => opt.MapFrom(s => s.By))
            .ForMember(d => d.Time, opt => opt.MapFrom(s => DateTimeOffset.FromUnixTimeSeconds(s.Time).DateTime))
            .ForMember(d => d.Score, opt => opt.MapFrom(s => s.Score))
            .ForMember(d => d.CommentCount, opt => opt.MapFrom(s => s.Kids.Length));
}