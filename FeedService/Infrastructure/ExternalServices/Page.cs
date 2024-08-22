namespace FeedService.Infrastructure.ExternalServices
{
    public class Page
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public string Type { get; set; }

        public string Publisher { get; set; }

        public List<string> Language { get; set; }
    }
}
