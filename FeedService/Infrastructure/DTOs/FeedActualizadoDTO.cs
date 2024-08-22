namespace FeedService.Infrastructure.DTOs
{
    public class FeedActualizadoDTO
    {
        public string Nombre { get; set; }
        public bool EsPublico { get; set; }
        public List<string> Topics { get; set; }
    }
}
