using Microsoft.EntityFrameworkCore;
using UsuarioService.Core.Models;

namespace UsuarioService.Core.Contexts
{
    public class UsuarioDBContext : DbContext
    {
        public UsuarioDBContext(DbContextOptions<UsuarioDBContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
