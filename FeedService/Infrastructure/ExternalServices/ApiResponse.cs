using Newtonsoft.Json;

namespace FeedService.Infrastructure.ExternalServices
{
    public class ApiResponse
    {
        [JsonProperty("items")]
        public List<Page> Pages { get; set; }
    }
}
