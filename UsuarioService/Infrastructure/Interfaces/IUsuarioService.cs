using UsuarioService.Infrastructure.DTOs;

namespace UsuarioService.Infrastructure.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetUsuarios();

        Task<UsuarioDTO> GetUsuario(int id);

        Task CrearUsuario(UsuarioDTO usuarioDTO);

        Task<UsuarioDTO> GetUsuario(UsuarioLogueoDTO usuarioLogueo);
    }
}
