using FeedService.Core.Interfaces;
using FeedService.Core.Models;
using FeedService.Infrastructure.DTOs;
using FeedService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FeedService.Infrastructure.Services
{
    public class FeedService : IFeedService
    {
        private readonly IFeedRepository _feedRepository;
        private readonly IMappingService _mappingService;
        private readonly IExternalApiService _externalApiService;

        public FeedService(IFeedRepository feedRepository, IMappingService mappingService, IExternalApiService externalApiService)
        {
            _feedRepository = feedRepository;
            _mappingService = mappingService;
            _externalApiService = externalApiService;
        }

        public async Task<List<FeedDTO>> GetFeeds(string topic, int numeroPagina, int tamañoPagina, bool ordernarPorActualizado, int? creadoPor)
        {
            // Validar la paginación
            if (numeroPagina < 1)
                numeroPagina = 1;

            if (tamañoPagina < 1)
                tamañoPagina = 10;

            try
            {
                var feeds = await _feedRepository.GetFeeds();                

                if (!string.IsNullOrEmpty(topic))
                {
                    feeds = feeds.Where(f => f.Topics.Contains(topic) && (f.EsPublico || f.CreadoPor == creadoPor));

                }

                if (ordernarPorActualizado)
                {
                    feeds = feeds.OrderByDescending(f => f.FechaActualizacion);
                }
                else
                {
                    feeds = feeds.OrderByDescending(f => f.FechaCreacion);
                }

                var feedsDTO = _mappingService.MapFeedsToDTO(feeds);

                // Aplicar paginación
                var filteredFeed = feedsDTO
                    .Skip((numeroPagina - 1) * tamañoPagina)
                    .Take(tamañoPagina)
                    .ToList();

                return filteredFeed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<FeedDTO>();
            }
        }

        public async Task<FeedDTO> GetFeedById(int id)
        {
            try
            {
                var feed = await _feedRepository.GetFeedById(id);

                var feedDTO = _mappingService.MapFeedToDTO(feed);

                return feedDTO;
            }
            catch (DataAccessException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }

        public async Task<FeedDetalleDTO> GetFeedDetalle(FeedDTO feedDTO)
        {
            try
            {
                const int NUMERO_RECURSOS = 20;

                var recursos = await _externalApiService.GetRecursosAsync(feedDTO.Topics.ToList(), NUMERO_RECURSOS);

                var feedDetalle = new FeedDetalleDTO
                {
                    Id = feedDTO.Id,
                    Nombre = feedDTO.Nombre,
                    EsPublico = feedDTO.EsPublico,
                    Topics = feedDTO.Topics.Select(t => t).ToList(),
                    FechaCreacion = feedDTO.FechaCreacion,
                    FechaActualizacion = feedDTO.FechaActualizacion,
                    Recursos = recursos,
                };

                feedDetalle.Recursos = feedDetalle.Recursos.OrderByDescending(r => r.Fecha).ToList();

                return feedDetalle;
            }
            catch (DataAccessException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }

        public async Task CrearFeed(FeedDTO feedDTO)
        {
            try
            {
                var feed = _mappingService.MapDTOToFeed(feedDTO);

                await _feedRepository.CrearFeed(feed);
            }
            catch (DataAccessException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }

        }

        public async Task ActualizarFeed(FeedDTO feedDTO, FeedActualizadoDTO feedActualizadoDTO)
        {
            try
            {
                if (!string.IsNullOrEmpty(feedActualizadoDTO.Nombre) && !feedDTO.Nombre.Trim().Equals(feedActualizadoDTO.Nombre.Trim()))
                {
                    feedDTO.Nombre = feedActualizadoDTO.Nombre;
                }

                if (feedDTO.EsPublico != feedActualizadoDTO.EsPublico)
                {
                    feedDTO.EsPublico = feedActualizadoDTO.EsPublico;
                }

                feedDTO.FechaActualizacion = DateTime.UtcNow;

                var feedActualizada = _mappingService.MapDTOToFeed(feedDTO);

                await _feedRepository.ActualizarFeed(feedActualizada);
            }
            catch (DataAccessException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }

        }

        public async Task BorrarFeed(int id)
        {
            try
            {
                //var feed = _mappingService.MapDTOToFeed(feedDTO);
                await _feedRepository.BorrarFeed(id);
            }
            catch (DataAccessException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }

        public async Task BorrarTopics(FeedDTO feedActualizadaDTO)
        {
            try
            {
                feedActualizadaDTO.FechaActualizacion = DateTime.UtcNow;

                var feedActualizada = _mappingService.MapDTOToFeed(feedActualizadaDTO);

                await _feedRepository.ActualizarFeed(feedActualizada);
            }
            catch (DataAccessException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }
    }
}
