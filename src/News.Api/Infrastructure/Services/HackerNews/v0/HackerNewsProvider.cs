using Microsoft.Extensions.Caching.Memory;
using News.Api.Infrastructure.Services.HackerNews.v0.Models;

namespace News.Api.Infrastructure.Services.HackerNews.v0;

public interface IHackerNewsProvider
{
    Task<uint[]?> GetAllBestStoriesIds(CancellationToken cancellationToken);
    Task<StoryResponse?> GetStory(uint id, CancellationToken cancellationToken);
}

public class HackerNewsProvider(
    ILogger<HackerNewsProvider> logger,
    IMemoryCache memoryCache,
    IHackerNewsHttpClient hackerNewsHttpClient) : IHackerNewsProvider
{
    private const string AllBestStoriesKey = "all-best-stories";
    private const string StoryKey = "story";
    private readonly TimeSpan _absoluteExpiration = TimeSpan.FromMinutes(5);

    private readonly ILogger<HackerNewsProvider> _logger = logger;

    public Task<uint[]?> GetAllBestStoriesIds(CancellationToken cancellationToken)
    {
        return memoryCache.GetOrCreateAsync(
            AllBestStoriesKey,
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(_absoluteExpiration);
                return hackerNewsHttpClient.GetBestStoriesIds(cancellationToken);

            });
    }

    public Task<StoryResponse?> GetStory(uint id, CancellationToken cancellationToken)
    {
        return memoryCache.GetOrCreateAsync(
            $"{StoryKey}-{id}",
            cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(_absoluteExpiration);
                return hackerNewsHttpClient.GetStory(id, cancellationToken);
            });
    }
}