using System.Text.Json.Serialization;

namespace News.Api.Application.Stories.v1;

public class GetBestStoriesResponse
{
    /// <summary>
    /// Stories title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Story Uri
    /// </summary>
    public string Uri { get; set; }

    /// <summary>
    /// Posted by
    /// </summary>
    public string PostedBy { get; set; }

    /// <summary>
    /// Time when story posted
    /// </summary>
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Time { get; set; }

    /// <summary>
    /// Story score
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Amount of comments in story
    /// </summary>
    public int CommentCount { get; set; }
}