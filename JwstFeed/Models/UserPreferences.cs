using JwstFeed.Models.FeedItems;

namespace JwstFeed.Models;

public class UserPreferences
{
    public PaginatedList<IFeedItem> PaginatedList { get; set; }

    public ResultFilter Filter { get; set; }

    public string SearchTerm { get; set; } = string.Empty;
}