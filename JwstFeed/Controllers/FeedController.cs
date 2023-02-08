using JwstFeed.Models;
using JwstFeed.Models.FeedItems;
using JwstFeed.Models.Providers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JwstFeed.Controllers;

public class FeedController : Controller
{
    #region Data Members
    private string filterCookieName { get; } = "lastFilter";
    private int pageSize { get; } = 75;
    #endregion

    #region Public Methods
    public IActionResult Index()
    {
        string lastFilter = getFilterFromCookie();

        UserPreferences userNewPreferences = generateUserPreferences(
            currentFilter: lastFilter,
            searchTerm: string.Empty,
            pageNumber: 1);

        return View("ShowFeed", userNewPreferences);
    }

    public IActionResult ShowFeed(string currentFilter, int pageNumber = 1, string searchTerm = "")
    {
        UserPreferences userNewPreferences = generateUserPreferences(
            currentFilter: currentFilter,
            pageNumber: pageNumber,
            searchTerm: searchTerm);

        return View(userNewPreferences);
    }

    [HttpPost]
    public IActionResult ShowFilteredFeed(UserPreferences userPreferences)
    {
        saveFilterInCookie(userPreferences.Filter);
        PaginatedList<IFeedItem> currentPageFeedItems = getFilteredList(userPreferences.Filter, pageNumber: 1);
        UserPreferences userNewPreferences = getUserPreferences(currentPageFeedItems, userPreferences.Filter);

        return View("ShowFeed", userNewPreferences);
    }

    [HttpPost]
    public IActionResult Search(string currentFilter, string searchTerm)
    {
        UserPreferences userNewPreferences = generateUserPreferences(
            currentFilter: currentFilter,
            pageNumber: 1,
            searchTerm: searchTerm);

        return View("ShowFeed", userNewPreferences);
    }
    #endregion

    #region Private Methods
    private UserPreferences generateUserPreferences(string currentFilter, int pageNumber, string searchTerm)
    {
        ResultFilter deserializedFilter = deserializeFilter(currentFilter);
        PaginatedList<IFeedItem> currentPageFeedItems = getFilteredList(deserializedFilter, pageNumber, searchTerm);
        UserPreferences userNewPreferences = getUserPreferences(currentPageFeedItems, deserializedFilter, searchTerm);

        return userNewPreferences;
    }

    private PaginatedList<IFeedItem> getFilteredList(ResultFilter filter, int pageNumber, string searchTerm = "")
    {
        FeedPageSettings pageSettings = new FeedPageSettings()
        {
            SearchTerm = searchTerm.Trim(),
            PageSize = this.pageSize,
            PageNumber = pageNumber,
            Filter = filter,
        };

        return getFeedItems(pageSettings);
    }

    private ResultFilter deserializeFilter(string filter)
    {
        Func<ResultFilter> deserialization = () =>
        {
            try
            {
                return JsonConvert.DeserializeObject<ResultFilter>(filter)
                    ?? new ResultFilter();
            }
            catch (Exception ex)
            {
                return new ResultFilter();
            }
        };

        return string.IsNullOrEmpty(filter)
            ? new ResultFilter()
            : deserialization.Invoke();
    }

    private void saveFilterInCookie(ResultFilter filter)
    {
        string deserializedFilter = filter.ToString();

        Response
            .Cookies
            .Append(
                key: this.filterCookieName,
                value: deserializedFilter,
                options: new CookieOptions()
                {
                    Expires = DateTime.Now.AddMonths(12)
                });
    }

    private PaginatedList<IFeedItem> getFeedItems(FeedPageSettings pageSettings)
        =>
        new FeedProvider()
        .GetFeedItems(pageSettings);

    private string getFilterFromCookie()
        =>
        Request.Cookies[this.filterCookieName] ?? string.Empty;

    private UserPreferences getUserPreferences(PaginatedList<IFeedItem> feedItems, ResultFilter filter, string searchTerm = "")
        =>
        new UserPreferences()
        {
            PaginatedList = feedItems,
            SearchTerm = searchTerm,
            Filter = filter
        };
    #endregion
}