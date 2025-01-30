using Microsoft.Extensions.Options;
using News.Api.Infrastructure.Services.HackerNews.v0.Models;
using System.Diagnostics.CodeAnalysis;

namespace News.Api.Infrastructure.Services.HackerNews.v0;

[ExcludeFromCodeCoverage]
public class HackerNewsHttpClient : IHackerNewsHttpClient
{
    private readonly HttpClient _httpClient;

    public HackerNewsHttpClient(HttpClient httpClient, IOptions<HackerNewsConfig> hackerNewsConfig)
    {
        _httpClient = httpClient;
        var hackerNewsConfig1 = hackerNewsConfig.Value;
        _httpClient.BaseAddress = new Uri($"{hackerNewsConfig1.Url.TrimEnd('/')}");
    }

    public async Task<uint[]?> GetBestStoriesIds(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<uint[]>("/v0/beststories.json", cancellationToken);
    }

    public async Task<StoryResponse?> GetStory(uint id, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<StoryResponse>($"/v0/item/{id}.json", cancellationToken);
    }
}