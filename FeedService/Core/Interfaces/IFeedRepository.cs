using FeedService.Core.Contexts;
using FeedService.Core.Models;

namespace FeedService.Core.Interfaces
{
    public interface IFeedRepository
    {
        Task<IQueryable<Feed>> GetFeeds();

        Task<Feed> GetFeedById(int id);

        Task CrearFeed(Feed feed);

        Task ActualizarFeed(Feed feed);

        Task BorrarFeed(int id);
    }
}
