using JwstFeed.Models;
using JwstFeed.Models.Extensions;
using JwstFeed.Models.Providers;
using Microsoft.AspNetCore.Mvc;

namespace JwstFeed.Controllers;

public class ScheduleController : Controller
{
    #region Data Members
    private int pageSize { get; } = 250;
    #endregion

    #region Public Methods
    public IActionResult Index()
    {
        ObservationSchedule observationSchedule = getObservationSchedule(pageNumber: 1);

        return View("ShowSchedule", observationSchedule);
    }

    public IActionResult ShowSchedule(int pageNumber = 1)
    {
        ObservationSchedule observationSchedule = getObservationSchedule(pageNumber);

        return View(observationSchedule);
    }
    #endregion

    #region Private Methods
    private ObservationSchedule getObservationSchedule(int pageNumber)
    {
        PaginatedList<Observation> observations = getObservations(pageNumber);
        TargetObservation currentObservation = getCurrentObservation(observations);
        TargetObservation nextObservation = getNextObservation(observations);

        return new ObservationSchedule()
        {
            ObservingNow = currentObservation,
            ObservingNext = nextObservation,
            Observations = observations
        };
    }

    private TargetObservation getCurrentObservation(IReadOnlyList<Observation> observations)
    {
        var currentObservation = observations
            .Select((o, i) => new
            {
                StartTime = o.ScheduledStartTime,
                EndTime = getEndTime(o),
                Observation = o,
                RowNumber = i
            })
            .FirstOrDefault(o => DateTime.UtcNow.Between(o.StartTime, o.EndTime));

        return new TargetObservation()
        {
            TargetName = currentObservation?.Observation.TargetName ?? "None",
            VisitID = currentObservation?.Observation.VisitID ?? string.Empty,
            RowNumber = currentObservation?.RowNumber ?? 0,
        };
    }

    private TargetObservation getNextObservation(IReadOnlyList<Observation> observations)
    {
        var nextObservation = observations
            .Select((o, i) => new
            {
                StartTime = o.ScheduledStartTime,
                EndTime = getEndTime(o),
                Observation = o,
                RowNumber = i
            })
            .LastOrDefault(o =>
            {
                return o.StartTime > DateTime.UtcNow
                          &&
                          !DateTime.UtcNow.Between(o.StartTime, o.EndTime);
            });

        return new TargetObservation()
        {
            TargetName = nextObservation?.Observation.TargetName ?? "None",
            VisitID = nextObservation?.Observation.VisitID ?? string.Empty,
            RowNumber = nextObservation?.RowNumber ?? 0,
        };
    }

    private DateTime getEndTime(Observation observation)
    {
        int days = observation.Duration.Split('/')[0].ToInt();
        string[] timespan = observation.Duration.Split('/')[1].Split(':');
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

    private PaginatedList<Observation> getObservations(int pageNumber)
    {
        BasePageSettings pageSettings = new ObservationPageSettings()
        {
            PageSize = this.pageSize,
            PageNumber = pageNumber
        };

        return new FeedProvider()
            .GetObservations(pageSettings);
    }
    #endregion
}