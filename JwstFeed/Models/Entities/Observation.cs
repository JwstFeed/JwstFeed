using System.ComponentModel.DataAnnotations;

namespace JwstFeed.Models.Entities;

public class Observation
{
    [Key]
    public string ClusterIndex { get; set; }

    public string VisitID { get; set; } = "VisitID";

    public string PcsMode { get; set; } = "PcsMode";

    public string VisitType { get; set; } = "VisitType";

    public DateTime ScheduledStartTime { get; set; }

    public string Duration { get; set; }

    public string ScienceInstumentAndMode { get; set; }

    public string TargetName { get; set; } = "TargetName";

    public string Category { get; set; } = "Category";

    public string KeyWords { get; set; } = "KeyWords";
}