using JwstFeed.Models.DAL;
using JwstFeed.Models.DAL.EntityModels;
using JwstFeed.Models.Extensions;
using JwstFeed.Models.FeedItems;
using Microsoft.EntityFrameworkCore;

namespace JwstFeed.Models.Providers;

public class FeedProvider
{
    #region Data Members
    private WebbFeedDbContext dbContext { get; }
    private IDictionary<string, int> enumKeys { get; } // For Faster Performance
    #endregion

    #region Ctor
    public FeedProvider()
    {
        this.dbContext = new WebbFeedDbContext();
    }
    #endregion

    #region Public Methods
    public PaginatedList<IFeedItem> GetFeedItems(FeedPageSettings pageSettings)
    {
        this.enumKeys = getEnumKeys();
        IQueryable<EntityFeedItemModel> filteredItems = getFilteredItems(pageSettings);
        ICollection<IFeedItem> convertedItems = getConvertedItems(filteredItems, pageSettings);
        int totalCount = filteredItems.Count();

        return new PaginatedList<IFeedItem>(convertedItems, totalCount, pageSettings);
    }

    public PaginatedList<Observation> GetObservations(BasePageSettings pageSettings)
    {
        IQueryable<Observation> observations = getObservationSchedule();
        IReadOnlyList<Observation> currentPageObservations = getCurrentPageObservations(observations, pageSettings);
        int observationsCount = observations.Count();

        return new PaginatedList<Observation>(currentPageObservations, observationsCount, pageSettings);
    }
    #endregion

    #region Private Methods
    private ICollection<IFeedItem> getConvertedItems(IQueryable<EntityFeedItemModel> filteredItems, FeedPageSettings pageSettings)
    {
        List<EntityFeedItemModel> currentPageItems = getCurrentPageItems(filteredItems, pageSettings);

        return convertItems(currentPageItems);
    }

    private ICollection<IFeedItem> convertItems(List<EntityFeedItemModel> items) // For Faster Performance
    {
        ICollection<IFeedItem> newItems = new List<IFeedItem>();

        foreach (var i in items)
        {
            newItems.Add(new FeedItem()
            {
                IconIndex = i.SourceTypeID.ToEnum<eIconIndex>(),
                PlotType = i.PlotTypeID.ToEnum<ePlotType>(),
                DatePublished = i.DatePublished,
                ThumbnailUrl = i.ThumbnailUrl,
                ShortTitle = i.ShortTitle,
                SourceUrl = i.SourceUrl,
                TargetUrl = i.PlotUrl,
            });
        }

        return newItems;
    }

    private IQueryable<EntityFeedItemModel> getFilteredItems(FeedPageSettings pageSettings)
        =>
        this.dbContext
        .FeedItems
        .AsNoTracking()
        .Where(i => !i.IsDirty)
        .Where(i =>
                   (pageSettings.Filter.IsToShowWebbTelescopeArticles && i.SourceTypeID == enumKeys["WebbTelescopeOrgImages"])
                || (pageSettings.Filter.IsToShowWebbOrgImages && i.SourceTypeID == enumKeys["WebbTelescopeOrg"])
                || (pageSettings.Filter.IsToShowTwitterRawBot && i.SourceTypeID == enumKeys["TwitterRawPhotoBot"])
                || (pageSettings.Filter.IsToShowStsciRawNirspec && i.SourceTypeID == enumKeys["StsciRawNirspec"])
                || (pageSettings.Filter.IsToShowStsciRawNircam && i.SourceTypeID == enumKeys["StsciRawNircam"])
                || (pageSettings.Filter.IsToShowStsciRawNiriss && i.SourceTypeID == enumKeys["StsciRawNiriss"])
                || (pageSettings.Filter.IsToShowStsciSchedule && i.SourceTypeID == enumKeys["StsciSchedule"])
                || (pageSettings.Filter.IsToShowStsciRawMiri && i.SourceTypeID == enumKeys["StsciRawMiri"])
                || (pageSettings.Filter.IsToShowNasaBlogs && i.SourceTypeID == enumKeys["NasaBlogs"])
                || (pageSettings.Filter.IsToShowEsaWebb && i.SourceTypeID == enumKeys["EsaWebb"])
                || (pageSettings.Filter.IsToShowYoutube && i.SourceTypeID == enumKeys["Youtube"])
                || (pageSettings.Filter.IsToShowTwitter && i.SourceTypeID == enumKeys["Twitter"])
                || (pageSettings.Filter.IsToShowReddit && i.SourceTypeID == enumKeys["Reddit"])
                || (pageSettings.Filter.IsToShowCeers && i.SourceTypeID == enumKeys["Ceers"])
                || (pageSettings.Filter.IsToShowFlickr && i.SourceTypeID == enumKeys["Flickr"])
                || (pageSettings.Filter.IsToShowArxiv && i.SourceTypeID == enumKeys["Arxiv"]))
        .Where(i =>
                string.IsNullOrEmpty(pageSettings.SearchTerm)
                || i.ShortTitle.ToUpper().Contains(pageSettings.SearchTerm.ToUpper()));

    private List<EntityFeedItemModel> getCurrentPageItems(IQueryable<EntityFeedItemModel> filteredItems, BasePageSettings pageSettings)
        =>
        filteredItems
        .OrderByDescending(i => i.ClusterIndex)
        .Skip((pageSettings.PageNumber - 1) * pageSettings.PageSize)
        .Take(pageSettings.PageSize)
        .ToList();

    private IQueryable<Observation> getObservationSchedule()
        =>
        this.dbContext
        .Observations;

    private IReadOnlyList<Observation> getCurrentPageObservations(IQueryable<Observation> observations, BasePageSettings pageSettings)
        =>
        observations
        .OrderByDescending(o => o.ClusterIndex)
        .Skip((pageSettings.PageNumber - 1) * pageSettings.PageSize)
        .Take(pageSettings.PageSize)
        .ToList();

    private IDictionary<string, int> getEnumKeys()
        =>
        Enum
        .GetValues(typeof(eIconIndex))
        .Cast<eIconIndex>()
        .ToDictionary(t => t.ToString(), t => (int)t);
    #endregion
}
