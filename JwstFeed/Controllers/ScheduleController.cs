using JwstFeed.Models.Entities;
using JwstFeed.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwstFeed.Controllers;

public class ScheduleController : Controller
{
    #region Data Members
    private int pageSize { get; } = 200;
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
        ObservationScheduleService service = getService(pageNumber);

        return new ObservationSchedule()
        {
            ObservingNow = service.GetCurrentObservation(),
            ObservingNext = service.GetNextObservation(),
            Observations = service.GetAllObservations()
        };
    }

    private ObservationScheduleService getService(int pageNumber)
        =>
        new ObservationScheduleService(pageNumber, this.pageSize);
    #endregion
}
