using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using UsuarioService.Core.Contexts;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            // Elimina la base de datos real
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UsuarioDBContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Agrega una base de datos en memoria
            services.AddDbContext<UsuarioDBContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            //services.AddDbContext<UsuarioDBContext>(options =>
            //options.UseInMemoryDatabase("TestDatabase"));

            // Construye el servicio de resolución
            var serviceProvider = services.BuildServiceProvider();

            // Crea el alcance y asegura que la base de datos esté en el estado esperado
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<UsuarioDBContext>();
                db.Database.EnsureCreated(); // Crea la base de datos nuevamente
            }
        });
    }
}
