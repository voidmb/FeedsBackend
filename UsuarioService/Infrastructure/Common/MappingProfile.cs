using AutoMapper;
using UsuarioService.Core.Models;
using UsuarioService.Infrastructure.DTOs;

namespace UsuarioService.Infrastructure.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();

            CreateMap<Usuario, UsuarioLogueoDTO>().ReverseMap();
        }
    }
}
