using FeedService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedService.Core.Contexts
{
    public class FeedContext : DbContext
    {
        public FeedContext(DbContextOptions<FeedContext> options) : base(options) { }

        public DbSet<Feed> Feeds { get; set; }
    }
}
