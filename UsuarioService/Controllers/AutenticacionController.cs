using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuarioService.Infrastructure.DTOs;
using UsuarioService.Infrastructure.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsuarioService _usuarioService;

    public AuthController(IConfiguration configuration, IUsuarioService usuarioService)
    {
        _configuration = configuration;
        _usuarioService = usuarioService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UsuarioLogueoDTO logueoDTO)
    {
        try
        {
            var usuarioLogueado = _usuarioService.GetUsuario(logueoDTO).Result;

            if (usuarioLogueado != null) //Realizar las consultas pertientes, consultar DB, etc.
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var signingKey = new SymmetricSecurityKey(key);
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, logueoDTO.Username),
                    new Claim(ClaimTypes.Role, "Administrador") // Seasigna el rol de Admin para que pueda ver el post
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = signingCredentials,
                    Audience = _configuration["Jwt:Issuer"],
                    Issuer = _configuration["Jwt:Issuer"]
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
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
