using Microsoft.EntityFrameworkCore;
using UsuarioService.Core.Contexts;
using UsuarioService.Core.Interfaces;
using UsuarioService.Core.Models;
using UsuarioService.Infrastructure.DTOs;

namespace UsuarioService.Core.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDBContext _usuarioDBContext;

        public UsuarioRepository(UsuarioDBContext usuarioDBContext)
        {
            _usuarioDBContext = usuarioDBContext;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            try
            {
                return await _usuarioDBContext.Set<Usuario>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            try
            {
                return await _usuarioDBContext.Usuarios.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }

        public async Task<Usuario> GetUsuario(UsuarioLogueoDTO usuario)
        {
            try
            {
                return await _usuarioDBContext.Usuarios.FirstOrDefaultAsync(u => u.Username == usuario.Username && u.Password == usuario.Password);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            try
            {
                _usuarioDBContext.Usuarios.Add(usuario);
                await _usuarioDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Ocurrió una excepción en el acceso a datos.", ex);
            }
        }
    }
}
