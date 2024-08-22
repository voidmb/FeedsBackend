using FeedService.Core.Contexts;
using FeedService.Core.Interfaces;
using FeedService.Core.Models;
using FeedService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedService.Core.Repositories
{
    public class FeedRepository : IFeedRepository
    {
        private readonly FeedContext _feedDBContext;

        public FeedRepository(FeedContext feedDBContext)
        {
            _feedDBContext = feedDBContext;
        }

        public async Task<IQueryable<Feed>> GetFeeds()
        {
            try
            {
                return _feedDBContext.Feeds;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }

        public async Task<Feed> GetFeedById(int id)
        {
            try
            {
                return await _feedDBContext.Feeds.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }

        public async Task CrearFeed(Feed feed)
        {
            try
            {
                _feedDBContext.Feeds.Add(feed);
                await _feedDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }

        public async Task ActualizarFeed(Feed feed)
        {
            try
            {
                var existingFeed = await _feedDBContext.Feeds.FindAsync(feed.Id);

                if (existingFeed != null)
                {
                    _feedDBContext.Entry(existingFeed).CurrentValues.SetValues(feed);

                    await _feedDBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }

        public async Task BorrarFeed(int id)
        {
            try
            {
                var feed = await _feedDBContext.Feeds.FindAsync(id);

                if (feed != null)
                {
                    // Eliminar la entidad
                    _feedDBContext.Feeds.Remove(feed);
                    await _feedDBContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }
    }
}
