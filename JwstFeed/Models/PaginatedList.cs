namespace JwstFeed.Models;

public class PaginatedList<T> : List<T>
{
    #region Properties
    public bool HasNextPage => PageIndex < TotalPages;
    public bool HasPreviousPage => PageIndex > 1;
    public int TotalPages { get; }
    public int PageIndex { get; }
    #endregion

    #region Ctor
    public PaginatedList(IEnumerable<T> items, int count, BasePageSettings pageSettings)
    {
        PageIndex = pageSettings.PageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSettings.PageSize);

        this.AddRange(items);
    }
    #endregion
}