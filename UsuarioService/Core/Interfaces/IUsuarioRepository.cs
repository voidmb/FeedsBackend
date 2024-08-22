using UsuarioService.Core.Models;
using UsuarioService.Infrastructure.DTOs;

namespace UsuarioService.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuarios();

        Task<Usuario> GetUsuario(int id);

        Task CrearUsuario(Usuario usuario);

        Task<Usuario> GetUsuario(UsuarioLogueoDTO usuario);
    }
}
