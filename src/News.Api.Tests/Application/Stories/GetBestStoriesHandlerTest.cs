using AutoMapper;
using Moq;
using News.Api.Application.Stories.v1;
using News.Api.Infrastructure.Services.HackerNews.v0;
using News.Api.Infrastructure.Services.HackerNews.v0.Models;

namespace News.Api.Tests.Application.Stories;

public class GetBestStoriesHandlerTest
{
    private readonly Mock<IHackerNewsProvider> _hackerNewsProvider = new(MockBehavior.Strict);
    private readonly Mock<IMapper> _mockMapper = new(MockBehavior.Strict);


    private readonly StoryResponse storyResponse1 = new StoryResponse
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

    private readonly GetBestStoriesResponse bestStoryResponse1 = new()
    {
        CommentCount = 3,
        Score = 104,
        PostedBy = "dhouston",
        Title = "My YC app: Dropbox - Throw away your USB drive",
        Uri = "https://opensource.googleblog.com/2025/01/see-code-that-powered-pebble-smartwatches.html"
    };

    private readonly GetBestStoriesResponse bestStoryResponse2 = new()
    {
        CommentCount = 4,
        Score = 1079,
        PostedBy = "james2doyle",
        Title = "I still like Sublime Text",
        Uri = "https://ohdoylerules.com/workflows/why-i-still-like-sublime-text-in-2025/"
    };


    private readonly StoryResponse storyResponse2 = new StoryResponse
    {
        By = "james2doyle",
        Descendants = 558,
        Id = 2,
        Kids = [1, 2, 3, 4],
        Score = 1079,
        Time = 1738133023,
        Title = "I still like Sublime Text",
        Type = "story",
        Url = "https://ohdoylerules.com/workflows/why-i-still-like-sublime-text-in-2025/"
    };

    [Fact]
    public async Task GetBestStories_Handle_Success()
    {

        _hackerNewsProvider.Setup(x => x.GetAllBestStoriesIds(It.IsAny<CancellationToken>()))
            .ReturnsAsync([1, 2]);
        _hackerNewsProvider.Setup(x => x.GetStory(1, It.IsAny<CancellationToken>())).ReturnsAsync(
            storyResponse1);
        _hackerNewsProvider.Setup(x => x.GetStory(2, It.IsAny<CancellationToken>())).ReturnsAsync(
            storyResponse2);

        _mockMapper.Setup(x => x.Map<GetBestStoriesResponse>(It.Is<StoryResponse>(y => y.Id == 1)))
            .Returns(bestStoryResponse1);
        _mockMapper.Setup(x => x.Map<GetBestStoriesResponse>(It.Is<StoryResponse>(y => y.Id == 2)))
            .Returns(bestStoryResponse2);

        var handler = new GetBestStoriesHandler(_hackerNewsProvider.Object, _mockMapper.Object);

        await handler.Handle(new GetBestStoriesQuery { Quantity = 2 }, CancellationToken.None);

        _hackerNewsProvider.VerifyAll();
        _mockMapper.VerifyAll();
    }

    [Fact]
    public async Task GetBestStories_Handle_NoStories_Success()
    {
        _hackerNewsProvider.Setup(x => x.GetAllBestStoriesIds(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var handler = new GetBestStoriesHandler(_hackerNewsProvider.Object, _mockMapper.Object);

        await handler.Handle(new GetBestStoriesQuery { Quantity = 1 }, CancellationToken.None);

        _hackerNewsProvider.Verify(x => x.GetStory(It.IsAny<uint>(), It.IsAny<CancellationToken>()), Times.Never);
        _hackerNewsProvider.VerifyAll();
        _mockMapper.VerifyAll();
    }
}