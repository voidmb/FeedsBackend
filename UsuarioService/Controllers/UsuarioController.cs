using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuarioService.Core.Models;
using UsuarioService.Infrastructure.DTOs;
using UsuarioService.Infrastructure.Interfaces;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsuarioDTO>>> GetUsuarios()
    {
        try
        {
            var usuarios = await _usuarioService.GetUsuarios();

            if (usuarios == null || !usuarios.Any())
            {
                return NotFound();
            }

            return Ok(usuarios);
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

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDTO>> GetUsuarioById(int id)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
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
    public async Task<IActionResult> CreateUser([FromBody] UsuarioDTO usuarioDTO)
    {
        try
        {
            await _usuarioService.CrearUsuario(usuarioDTO);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuarioDTO.Id }, usuarioDTO);
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
