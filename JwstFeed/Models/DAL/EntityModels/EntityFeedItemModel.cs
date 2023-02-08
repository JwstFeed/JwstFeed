using System.ComponentModel.DataAnnotations;

namespace JwstFeed.Models.DAL.EntityModels;

public class EntityFeedItemModel
{
    [Key]
    public string ClusterIndex { get; set; }

    public DateTime DatePublished { get; set; }

    public bool IsDirty { get; set; } = false;

    public int PlotTypeID { get; set; }

    public string PlotUrl { get; set; }

    public string ShortTitle { get; set; }

    public int SourceTypeID { get; set; }

    public string SourceUrl { get; set; }

    public string ThumbnailUrl { get; set; }

    public string UniqueID { get; set; }
}