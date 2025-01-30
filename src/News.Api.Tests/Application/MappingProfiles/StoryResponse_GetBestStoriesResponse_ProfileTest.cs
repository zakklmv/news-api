using AutoMapper;
using FluentAssertions;
using News.Api.Application.MappingProfiles;
using News.Api.Application.Stories.v1;
using News.Api.Infrastructure.Services.HackerNews.v0.Models;

namespace News.Api.Tests.Application.MappingProfiles;

public class StoryResponse_GetBestStoriesResponse_ProfileTest
{
    private readonly IMapper _mapper;
    public StoryResponse_GetBestStoriesResponse_ProfileTest() =>
        _mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile<StoryResponse_GetBestStoriesResponse_Profile>()).CreateMapper();

    [Fact]
    public void ShouldBe_Mapping_Success()
    {
        var storyResponse = new StoryResponse
        {
            By = "dhouston",
            Descendants = 71,
            Id = 1,
            Kids = [1, 2, 3],
            Score = 104,
            Time = 1175714200,
            Title = "My YC app: Dropbox - Throw away your USB drive",
            Type = "story",
            Url = "https://opensource.googleblog.com/2025/01/see-code-that-powered-pebble-smartwatches.html"
        };

        var expectedResult = new GetBestStoriesResponse()
        {
            CommentCount = 3,
            Score = 104,
            PostedBy = "dhouston",
            Title = "My YC app: Dropbox - Throw away your USB drive",
            Uri = "https://opensource.googleblog.com/2025/01/see-code-that-powered-pebble-smartwatches.html",
            Time = new DateTime(2007, 4, 4, 19, 16, 40)
        };

        var actualResult = _mapper.Map<GetBestStoriesResponse>(storyResponse);

        actualResult.Should().BeEquivalentTo(expectedResult);

    }
}