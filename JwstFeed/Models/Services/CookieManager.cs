using JwstFeed.Models.Entities;

namespace JwstFeed.Models.Services;

public class CookieManager
{
	#region Data Members
	private string filterCookieName { get; set; }
	#endregion

	#region Public Methods
	public CookieManager SetFilterCookieName(string filterCookieName)
	{
		this.filterCookieName = filterCookieName;

		return this;
	}

	public void HandleFilterMemory(UserPreferences userPreferences, string btnValue, HttpResponse response)
	{
		bool isToRemember = btnValue != "*";

		if (isToRemember)
		{
			saveFilterInCookie(userPreferences.Filter, response);
		}
	}

	public string GetFilterFromCookie(HttpRequest request)
		=>
		request.Cookies[this.filterCookieName] ?? string.Empty;
	#endregion

	#region Private Methods
	private void saveFilterInCookie(ResultFilter filter, HttpResponse response)
	{
		string deserializedFilter = filter.ToString();

		response
			.Cookies
			.Append(
				key: this.filterCookieName,
				value: deserializedFilter,
				options: new CookieOptions()
				{
					Expires = DateTime.Now.AddMonths(12)
				});
	}
	#endregion
}
