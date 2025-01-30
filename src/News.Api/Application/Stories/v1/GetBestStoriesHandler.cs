using AutoMapper;
using MediatR;
using News.Api.Infrastructure.Services.HackerNews.v0;

namespace News.Api.Application.Stories.v1;

public class GetBestStoriesHandler(IHackerNewsProvider hackerNewsProvider, IMapper mapper)
    : IRequestHandler<GetBestStoriesQuery, GetBestStoriesResponse[]>
{
    public async Task<GetBestStoriesResponse[]> Handle(GetBestStoriesQuery request, CancellationToken cancellationToken)
    {
        var bestStoriesIds = await hackerNewsProvider.GetAllBestStoriesIds(cancellationToken);
        if (bestStoriesIds == null || !bestStoriesIds.Any())
            return [];

        var tasks = bestStoriesIds.Take(request.Quantity).Select(
           async storyId =>
           {
               var story = await hackerNewsProvider.GetStory(storyId, cancellationToken);
               return mapper.Map<GetBestStoriesResponse>(story);
           }).ToArray();

        var result = await Task.WhenAll(tasks);

        return result;
    }
}