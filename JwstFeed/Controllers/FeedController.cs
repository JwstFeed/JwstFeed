using JwstFeed.Models.Entities;
using JwstFeed.Models.Services;
using Microsoft.AspNetCore.Mvc;

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
        UserPreferences userNewPreferences = getDefaultUserPreferences();

        return View("ShowFeed", userNewPreferences);
	}

	public IActionResult ShowFeed(string currentFilter, int pageNumber = 1, string searchTerm = "")
	{
		UserPreferences userNewPreferences = generateUserPreferences(
			pageNumber: pageNumber > 0 ? pageNumber : 1,
			currentFilter: currentFilter,
			searchTerm: searchTerm);

		return View(userNewPreferences);
	}

    public IActionResult ShowFilteredFeed()
	{
        UserPreferences userNewPreferences = getDefaultUserPreferences();

        return View("ShowFeed", userNewPreferences);
    }

    public IActionResult Search()
	{
        UserPreferences userNewPreferences = getDefaultUserPreferences();

        return View("ShowFeed", userNewPreferences);
    }

    [HttpPost]
	public IActionResult ShowFilteredFeed(UserPreferences userPreferences, string btnValue = "")
	{
		handleFilterMemory(userPreferences, btnValue);
		UserPreferences userNewPreferences = convertFilterToUserPrefs(userPreferences.Filter);

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
	private UserPreferences getDefaultUserPreferences()
	{
		string lastFilter = getFilterFromCookie();

		return generateUserPreferences(
			 currentFilter: lastFilter,
			 searchTerm: string.Empty,
			 pageNumber: 1);
	}

	private UserPreferences generateUserPreferences(string currentFilter, int pageNumber, string searchTerm)
		=>
		new UserPreferencesGenerator()
		.SetPageSize(this.pageSize)
		.GenerateUserPreferences(currentFilter, pageNumber, searchTerm);

	private UserPreferences convertFilterToUserPrefs(ResultFilter filter)
		=>
		new UserPreferencesGenerator()
		.SetPageSize(this.pageSize)
		.ConvertFilterToUserPrefs(filter);

	private void handleFilterMemory(UserPreferences userPreferences, string btnValue)
		=>
		new CookieManager()
		.SetFilterCookieName(this.filterCookieName)
		.HandleFilterMemory(userPreferences, btnValue, base.Response);

	private string getFilterFromCookie()
		=>
		new CookieManager()
		.SetFilterCookieName(this.filterCookieName)
		.GetFilterFromCookie(base.Request);
	#endregion
}
