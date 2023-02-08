namespace JwstFeed.Models.FeedItems;

public class FeedItem : IFeedItem
{
    public DateTime DatePublished { get; set; }

    public eIconIndex IconIndex { get; set; }

    public ePlotType PlotType { get; set; }

    public string ShortTitle { get; set; }

    public string SourceUrl { get; set; }

    public string TargetUrl { get; set; }

    public string ThumbnailUrl { get; set; }
}