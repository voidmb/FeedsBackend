using FeedService.Core.Models;
using FeedService.Core.Contexts;
using System;
using System.Linq;

namespace UserManagementApi.Data
{
    public static class SeedData
    {
        public static void Initialize(FeedContext context)
        {
            if (context.Feeds.Any())
            {
                return;   // DB has been seeded
            }

            var feed = new Feed
            {
                CreadoPor = 1,
                Nombre = "Soports",
                EsPublico = true,
                Topics = new List<string> {"swimming", "cycling", "tennis", "boxing", "shooting" },
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            context.Feeds.Add(feed);
            context.SaveChanges();
        }
    }
}
