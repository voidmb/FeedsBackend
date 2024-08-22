using FeedService.Infrastructure.DTOs;

namespace FeedService.Infrastructure.Interfaces
{
    public interface IExternalApiService
    {
        Task<List<RecursoDTO>> GetRecursosAsync(List<string> sports, int noRecursos);
    }
}
