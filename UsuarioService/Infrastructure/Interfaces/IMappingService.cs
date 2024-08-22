using UsuarioService.Core.Models;
using UsuarioService.Infrastructure.DTOs;

namespace UsuarioService.Infrastructure.Interfaces
{
    public interface IMappingService
    {
        IEnumerable<UsuarioDTO> MapUsuariosToDTO(IEnumerable<Usuario> modelList);

        UsuarioDTO MapUsuarioToDTO(Usuario model);

        Usuario MapUsuarioDTOToUsuario(UsuarioDTO dto);

        Usuario MapUsuarioToUsuarioLogueoDTO(UsuarioLogueoDTO dto);

        UsuarioLogueoDTO MapUsuarioLogueoDTOToUsuario(Usuario model);
    }
}
