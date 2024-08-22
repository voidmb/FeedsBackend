namespace UsuarioService.Infrastructure.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsAuthorized { get; set; }

        public DateTime LastAccess { get; set; } = DateTime.MinValue;
    }
}
