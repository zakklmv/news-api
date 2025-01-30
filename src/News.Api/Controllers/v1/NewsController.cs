using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using News.Api.Application.Stories.v1;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace News.Api.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v1/[controller]")]
[ExcludeFromCodeCoverage]
public class NewsController(ILogger<NewsController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<NewsController> _logger = logger;

    /// <summary>
    /// Returns best stories
    /// </summary>
    /// <param name="request">Quantity of the best stories needed</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>></param>
    /// <returns></returns>
    [HttpGet(Name = "best")]
    [SwaggerOperation(OperationId = nameof(GetBestStories), Summary = "Returns best stories")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetBestStoriesResponse[]))]
    public async Task<IActionResult> GetBestStories([FromQuery, BindRequired,
                                                     SwaggerParameter("Quantity of the best stories")]
        GetBestStoriesQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(request, cancellationToken));
    }
}