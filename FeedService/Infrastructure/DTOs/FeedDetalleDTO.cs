namespace FeedService.Infrastructure.DTOs
{
    public class FeedDetalleDTO
    {
        public int Id { get; set; }
        public int CreadoPor { get; set; }
        public string Nombre { get; set; }
        public bool EsPublico { get; set; }
        public List<string> Topics { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public List<RecursoDTO> Recursos { get; set; }
    }
}
