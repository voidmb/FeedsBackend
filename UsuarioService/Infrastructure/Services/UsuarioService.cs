using UsuarioService.Core.Interfaces;
using UsuarioService.Infrastructure.DTOs;
using UsuarioService.Infrastructure.Interfaces;
using static UsuarioService.Infrastructure.Interfaces.IMappingService;

namespace UsuarioService.Infrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {        
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMappingService _mappingService;


        public UsuarioService(IUsuarioRepository usuarioRepository, IMappingService mappingService)
        {
            _usuarioRepository = usuarioRepository;
            _mappingService = mappingService;
        }

        public async Task<List<UsuarioDTO>> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetUsuarios();

                var usuariosDTO = _mappingService.MapUsuariosToDTO(usuarios);

                return usuariosDTO.ToList();
            }
            catch (DataAccessException ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }

        public async Task<UsuarioDTO> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuario(id);

                var usuarioDTO = _mappingService.MapUsuarioToDTO(usuario);

                return usuarioDTO;
            }
            catch (DataAccessException ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }

        public async Task<UsuarioDTO> GetUsuario(UsuarioLogueoDTO usuarioLogueo)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuario(usuarioLogueo);

                var usuarioDTO = _mappingService.MapUsuarioToDTO(usuario);

                return usuarioDTO;
            }
            catch (DataAccessException ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }

        public async Task CrearUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = _mappingService.MapUsuarioDTOToUsuario(usuarioDTO);

                await _usuarioRepository.CrearUsuario(usuario);
              
                //return usuarioDTO;
            }
            catch (DataAccessException ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la petición.", ex);
            }
        }
    }
}
