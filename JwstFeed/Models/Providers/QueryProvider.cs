using JwstFeed.Models.DAL;
using JwstFeed.Models.DAL.EntityModels;
using JwstFeed.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwstFeed.Models.Providers;

public static class QueryProvider
{
    public static Func<WebbFeedDbContext, string, EntityFeedItemModel?> GetItem
        => EF.CompileQuery((WebbFeedDbContext context, string clusterIndex)
            => context
                .FeedItems
                .FirstOrDefault(i => i.ClusterIndex == clusterIndex));

    public static Func<WebbFeedDbContext, LatestImage> GetLastImage
        => EF.CompileQuery((WebbFeedDbContext context)
            => context
                .FeaturedImage
                .First());
}
