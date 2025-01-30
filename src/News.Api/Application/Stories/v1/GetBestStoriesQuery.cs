using MediatR;
using System.ComponentModel.DataAnnotations;

namespace News.Api.Application.Stories.v1;

public class GetBestStoriesQuery : IRequest<GetBestStoriesResponse[]>
{
    /// <summary>
    /// Quantity of the best stories"
    /// </summary>
    [Required]
    public int Quantity { get; set; }
}