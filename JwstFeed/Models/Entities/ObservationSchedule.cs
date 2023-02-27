namespace JwstFeed.Models.Entities;

public class ObservationSchedule
{
    public PaginatedList<Observation> Observations { get; set; }

    public TargetObservation ObservingNow { get; set; }

    public TargetObservation ObservingNext { get; set; }
}