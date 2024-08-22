using FeedService.Core.Contexts;
using FeedService.Infrastructure.DTOs;
using FeedService.Infrastructure.Interfaces;
using FeedService.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using Feed = FeedService.Core.Models.Feed;

[ApiController]
[Route("[controller]")]
[Authorize] // Se requiere JWT autenticación para todos los métodos
public class FeedsController : ControllerBase
{
    private readonly FeedContext _feedDBContext;
    private readonly IExternalApiService _externalApiService;
    private readonly IFeedService _feedService;

    public FeedsController(FeedContext feedDBcontext, IExternalApiService externalApiService, IFeedService feedService)
    {
        _feedDBContext = feedDBcontext;
        _feedService = feedService;
        _externalApiService = externalApiService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Feed>>> GetFeeds(
        [FromQuery] string topic = null,
        [FromQuery] int numeroPagina = 1,
        [FromQuery] int tamañoPagina = 10,
        [FromQuery] bool ordernarPorActualizado = true,
        [FromQuery] int? creadoPor = null)
    {
        try
        {
            List<FeedDTO> filteredFeeds = await _feedService.GetFeeds(topic, numeroPagina, tamañoPagina, ordernarPorActualizado, creadoPor);


            if (filteredFeeds == null || !filteredFeeds.Any())
            {
                return NotFound();
            }

            return Ok(filteredFeeds);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("Detalle/{id}")]
    [Authorize(Policy = "IsAdmin")] // Requiere rol de Admin para este método
    public async Task<ActionResult<FeedDetalleDTO>> GetFeed(int id)
    {
        try
        {
            var feedDTO = await _feedService.GetFeedById(id);

            if (feedDTO == null)
            {
                return NotFound();
            }

            var feedDetalle = await _feedService.GetFeedDetalle(feedDTO);

            return Ok(feedDetalle);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeed([FromBody] FeedDTO feed)
    {
        if (feed == null)
        {
            return BadRequest("No existe información para el Feed.");
        }

        if (feed.CreadoPor < 1)
        {
            return BadRequest("El Feed tiene que tener el id de usuario asociado.");
        }
        if (feed.Topics != null)
        {
            if (feed.Topics.Count < 1 || feed.Topics.Count > 5 || feed.Topics.Distinct().Count() != feed.Topics.Count)
            {
                return BadRequest("El feed debe de tener de 1 a 5 topics únicos.");
            }

            var uniqueTopics = feed.Topics.Distinct().ToList();

            if (uniqueTopics.Count != feed.Topics.Count)
            {
                return BadRequest("Los topics deben de ser únicos para cada feed.");
            }
        }

        try
        {
            await _feedService.CrearFeed(feed);

            return CreatedAtAction(nameof(GetFeed), new { id = feed.Id }, feed);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFeed(int id, [FromBody] FeedActualizadoDTO feedActualizadoDTO)
    {
        if (feedActualizadoDTO == null)
        {
            return BadRequest("No existe información para atualizar el feed.");
        }

        try
        {
            var feedDTO = await _feedService.GetFeedById(id);

            if (feedDTO == null)
            {
                return NotFound("No existe un feed con ese id.");
            }

            if (feedActualizadoDTO.Topics != null)
            {
                if (feedActualizadoDTO.Topics.Count < 1 || feedActualizadoDTO.Topics.Count > 5 || feedActualizadoDTO.Topics.Distinct().Count() != feedActualizadoDTO.Topics.Count)
                {
                    return BadRequest("El feed debe de tener de 1 a 5 topics únicos.");
                }

                feedDTO.Topics = feedActualizadoDTO.Topics;
            }

            await _feedService.ActualizarFeed(feedDTO, feedActualizadoDTO);

            return Ok(feedDTO);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeed(int id)
    {
        try
        {
            var feedDTO = await _feedService.GetFeedById(id);

            if (feedDTO == null)
            {
                return NotFound("Feed no encontrado.");
            }

            await _feedService.BorrarFeed(feedDTO.Id);

            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPatch("EliminarTopics{id}/topics")]
    public async Task<IActionResult> RemoveTopics(int id, [FromBody] List<string> topicsToDelete)
    {
        if (topicsToDelete == null || topicsToDelete.Count == 0)
        {
            return BadRequest("La lista de topics a eliminar no puede estar vacía.");
        }

        try
        {
            var feedDTO = await _feedService.GetFeedById(id);

            if (feedDTO == null)
            {
                return NotFound("Feed no encontrado.");
            }

            feedDTO.Topics.RemoveAll(topic => topicsToDelete.Contains(topic));

            if (feedDTO.Topics.Count < 1 || feedDTO.Topics.Count > 5)
            {
                return BadRequest("El Feed debe de contener entre 1 y 5 topics.");
            }

            await _feedService.BorrarTopics(feedDTO);

            return Ok(feedDTO);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }
}
