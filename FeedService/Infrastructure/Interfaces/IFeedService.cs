using FeedService.Core.Models;
using FeedService.Infrastructure.DTOs;

namespace FeedService.Infrastructure.Interfaces
{
    public interface IFeedService
    {
        Task<List<FeedDTO>> GetFeeds(string topic, int numeroPagina, int tamañoPagina, bool ordernarPorActualizado, int? creadoPor);

        Task<FeedDTO> GetFeedById(int id);

        Task<FeedDetalleDTO> GetFeedDetalle(FeedDTO feedDTO);

        Task CrearFeed(FeedDTO feedDTO);

        Task ActualizarFeed(FeedDTO feedDTO, FeedActualizadoDTO feedActualizadoDTO);

        Task BorrarFeed(int id);

        Task BorrarTopics(FeedDTO feedDTO);
    }
}
