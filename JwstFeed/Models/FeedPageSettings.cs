namespace JwstFeed.Models;

public class FeedPageSettings : BasePageSettings
{
    public ResultFilter Filter { get; set; }

    public string SearchTerm { get; set; } = string.Empty;
}