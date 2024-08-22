using AutoMapper;
using UsuarioService.Core.Models;
using UsuarioService.Infrastructure.DTOs;
using UsuarioService.Infrastructure.Interfaces;

namespace UsuarioService.Infrastructure.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<UsuarioDTO> MapUsuariosToDTO(IEnumerable<Usuario> modelList)
        {
            return _mapper.Map<IEnumerable<UsuarioDTO>>(modelList);
        }

        public UsuarioDTO MapUsuarioToDTO(Usuario model)
        {
            return _mapper.Map<UsuarioDTO>(model);
        }

        public Usuario MapUsuarioDTOToUsuario(UsuarioDTO dto)
        {
            return _mapper.Map<Usuario>(dto);
        }

        public Usuario MapUsuarioToUsuarioLogueoDTO(UsuarioLogueoDTO dto)
        {
            return _mapper.Map<Usuario>(dto);
        }
        public UsuarioLogueoDTO MapUsuarioLogueoDTOToUsuario(Usuario model)
        {
            return _mapper.Map<UsuarioLogueoDTO>(model);
        }
    }
}
