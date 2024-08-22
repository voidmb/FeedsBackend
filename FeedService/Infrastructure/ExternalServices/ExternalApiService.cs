using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using FeedService.Infrastructure;
using FeedService.Infrastructure.DTOs;
using FeedService.Infrastructure.ExternalServices;
using FeedService.Infrastructure.Interfaces;
using Newtonsoft.Json; // Asegúrate de agregar el paquete Newtonsoft.Json

public class ExternalApiService : IExternalApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://chroniclingamerica.loc.gov/search/pages/results";

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RecursoDTO>> GetRecursosAsync(List<string> sports, int noRecursos)
    {
        // Combina los términos de búsqueda con OR y codifica los espacios
        //var searchTerms = string.Join(" OR ", sports);
        //var encodedTerms = Uri.EscapeDataString(searchTerms);
        var searchTerms = string.Join(",", sports);

        // Construye la URL con los parámetros dados
        //var query = $"proxtext={encodedTerms}&rows={noRecursos}&start=0&sort=date&format=json";
        var query = $"any={searchTerms}&rows={noRecursos}&start=0&sort=date&format=json";
        var requestUrl = $"{BaseUrl}?{query}";

        string formato = "yyyyMMdd";

        try
        {
            var response = await _httpClient.GetStringAsync(requestUrl);

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

            var resources = apiResponse?.Pages?.Select(page => new RecursoDTO
            {
                Id = page.Id,
                Titulo = page.Title,
                Fecha = DateTime.ParseExact(page.Date, formato, CultureInfo.InvariantCulture),//DateTime.Parse(page.Date),
                Tipo = page.Type,
                Editorial = page.Publisher,
                Lenguaje = page.Language.FirstOrDefault() ?? "No Especificado",
            }).ToList();

            return resources ?? new List<RecursoDTO>();
        }
        catch (Exception ex)
        {
            // Maneja errores (podrías registrar el error o manejarlo de alguna otra manera)
            Console.WriteLine($"Error: {ex.Message}");
            return new List<RecursoDTO>();
        }
    }

    //public async Task<List<RecursoDTO>> GetRecursosAsync(List<string> sports, int noRecuersos)
    //{
    //    var query = "proxtext=boxing%20OR%20baseball%20OR%20football&rows=20&start=0&sort=date&format=json";

    //    var requestUrl = $"{ApiUrl}?{query}";

    //    var response = await _httpClient.GetStringAsync(requestUrl);

    //    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

    //    var resources = apiResponse?.Pages?.Select(page => new RecursoDTO
    //    {
    //        Titulo = page.Title,
    //        Fecha = DateTime.Parse(page.Date),
    //        Tipo = page.Type,
    //        Editorial = page.Editorial,
    //        Lenguaje = page.Languages
    //    }).ToList();

    //    return resources ?? new List<RecursoDTO>();
    //}
}

//    private class ApiResponse
//    {
//        [JsonProperty("items")]
//        public List<Page> Pages { get; set; }
//    }

//    private class Page
//    {
//        public string Title { get; set; }
//        public string Date { get; set; }
//        public string Type { get; set; }
//        public string Editorial { get; set; }
//        public string Languages { get; set; }
//    }
//}
