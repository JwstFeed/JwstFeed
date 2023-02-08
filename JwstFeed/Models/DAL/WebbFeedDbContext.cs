using JwstFeed.Models.DAL.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace JwstFeed.Models.DAL;

public class WebbFeedDbContext : DbContext
{
    #region Properties
    public DbSet<EntityFeedItemModel> FeedItems { get; set; }
    public DbSet<Observation> Observations { get; set; }
    #endregion

    #region Methods
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        string connStr = "*";

        builder.UseSqlServer(connStr);
    }
    #endregion
}