namespace JwstFeed.Models.Entities;

public class FeedPageSettings : BasePageSettings
{
    public ResultFilter Filter { get; set; }

    public string SearchTerm { get; set; } = string.Empty;
}