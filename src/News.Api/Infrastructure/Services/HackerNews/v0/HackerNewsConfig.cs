using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace News.Api.Infrastructure.Services.HackerNews.v0;

[ExcludeFromCodeCoverage]
public class HackerNewsConfig
{
    [Required]
    public string Url { get; set; }
}