using News.Api.Infrastructure.Services.HackerNews.v0.Models;

namespace News.Api.Infrastructure.Services.HackerNews.v0;

public interface IHackerNewsHttpClient
{
    Task<uint[]?> GetBestStoriesIds(CancellationToken cancellationToken);
    Task<StoryResponse?> GetStory(uint id, CancellationToken cancellationToken);
}