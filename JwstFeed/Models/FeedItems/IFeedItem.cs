namespace JwstFeed.Models.FeedItems;

public interface IFeedItem
{
    DateTime DatePublished { get; }

    eIconIndex IconIndex { get; set; }

    ePlotType PlotType { get; }

    string ShortTitle { get; set; }

    string SourceUrl { get; set; }

    string TargetUrl { get; set; }

    string ThumbnailUrl { get; }
}