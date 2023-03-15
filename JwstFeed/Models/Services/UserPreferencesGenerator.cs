using JwstFeed.Models.Entities;
using JwstFeed.Models.FeedItems;
using JwstFeed.Models.Providers;
using Newtonsoft.Json;

namespace JwstFeed.Models.Services;

public class UserPreferencesGenerator
{
	#region Data Members
	private int pageSize { get; set; }
	#endregion

	#region Public Methods
	public UserPreferencesGenerator SetPageSize(int pageSize)
	{
		this.pageSize = pageSize;

		return this;
	}

	public UserPreferences GenerateUserPreferences(string currentFilter, int pageNumber, string searchTerm)
	{
		ResultFilter deserializedFilter = deserializeFilter(currentFilter);
		PaginatedList<IFeedItem> currentPageFeedItems = getFilteredList(deserializedFilter, pageNumber, searchTerm);
		UserPreferences userNewPreferences = getUserPreferences(currentPageFeedItems, deserializedFilter, searchTerm);

		return userNewPreferences;
	}

	public UserPreferences ConvertFilterToUserPrefs(ResultFilter filter)
	{
		PaginatedList<IFeedItem> currentPageFeedItems = getFilteredList(filter, pageNumber: 1, searchTerm: string.Empty);

		return getUserPreferences(currentPageFeedItems, filter, searchTerm: string.Empty);
	}
	#endregion

	#region Private Methods
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

	private PaginatedList<IFeedItem> getFeedItems(FeedPageSettings pageSettings)
		=>
		new FeedProvider()
		.GetFeedItems(pageSettings);

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
