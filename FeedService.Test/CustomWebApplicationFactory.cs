using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using FeedService.Core.Contexts;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            // Elimina la base de datos real
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FeedContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Agrega una base de datos en memoria
            services.AddDbContext<FeedContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Construye el servicio de resolución
            var serviceProvider = services.BuildServiceProvider();

            // Crea el alcance y asegura que la base de datos esté en el estado esperado
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<FeedContext>();
                db.Database.EnsureCreated();
            }
        });
    }
}
