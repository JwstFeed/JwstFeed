using JwstFeed.Models.Entities;
using JwstFeed.Models.Extensions;
using JwstFeed.Models.Providers;

namespace JwstFeed.Models.Services;

public class ObservationScheduleService
{
	#region Data Members
	private PaginatedList<Observation> observations { get; }
    #endregion

    #region Ctor
    public ObservationScheduleService(int pageNumber, int pageSize)
    {
        this.observations = getObservations(pageNumber, pageSize);
    }
    #endregion

    #region Public Methods
    public TargetObservation GetCurrentObservation()
    {
        var currentObservation = this.observations
            .Select(o => new
            {
                StartTime = o.ScheduledStartTime,
                EndTime = getEndTime(o),
                Observation = o
            })
            .FirstOrDefault(o =>
            {
                return DateTime.UtcNow.Between(o.StartTime, o.EndTime);
            });

        return new TargetObservation()
        {
            KeyWords = currentObservation?.Observation?.KeyWords ?? string.Empty,
            TargetName = currentObservation?.Observation.TargetName ?? "None",
            VisitID = currentObservation?.Observation.VisitID ?? string.Empty
        };
    }

    public TargetObservation GetNextObservation()
    {
        var nextObservation = this.observations
            .Select(o => new
            {
                StartTime = o.ScheduledStartTime,
                EndTime = getEndTime(o),
                Observation = o
            })
            .LastOrDefault(o =>
            {
                return o.StartTime > DateTime.UtcNow
                       &&
                       !DateTime.UtcNow.Between(o.StartTime, o.EndTime);
            });

        return new TargetObservation()
        {
            KeyWords = nextObservation?.Observation?.KeyWords ?? string.Empty,
            TargetName = nextObservation?.Observation.TargetName ?? "None",
            VisitID = nextObservation?.Observation.VisitID ?? string.Empty
        };
    }

    public PaginatedList<Observation> GetAllObservations()
    {
        return this.observations;
    }   
    #endregion

    #region Private Methods
    private PaginatedList<Observation> getObservations(int pageNumber, int pageSize)
    {
        BasePageSettings pageSettings = new ObservationPageSettings()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return new FeedProvider()
            .GetObservations(pageSettings);
    }

    private DateTime getEndTime(Observation observation)
    {
        string[] timespan = observation.Duration.Split('/')[1].Split(':');
        int days = observation.Duration.Split('/')[0].ToInt();
        int hours = timespan[0].ToInt();
        int minutes = timespan[1].ToInt();
        int seconds = timespan[2].ToInt();

        return observation
            .ScheduledStartTime
            .AddDays(days)
            .AddHours(hours)
            .AddMinutes(minutes)
            .AddSeconds(seconds);
    }
    #endregion
}