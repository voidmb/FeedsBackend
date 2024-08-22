using System;
using System.Linq;
using UsuarioService.Core.Contexts;
using UsuarioService.Core.Models;

namespace UserManagementApi.Data
{
    public static class SeedData
    {
        public static void Initialize(UsuarioDBContext context)
        {
            if (context.Usuarios.Any())
            {
                return;  
            }

            var usuario = new Usuario
            {
                Username = "kiosko",
                Password = "kiosko", 
                IsAuthorized = true,
                LastAccess = DateTime.UtcNow
            };

            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }
    }
}
