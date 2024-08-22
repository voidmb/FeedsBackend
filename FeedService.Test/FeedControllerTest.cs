using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using FeedService.Infrastructure.DTOs;
using System.Net.Http.Headers;

namespace FeedService.Test
{
    public class FeedControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public FeedControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private string GetJwtToken()
        {
            // Cambia estos valores para coincidir con tu configuración
            var secretKey = "3e7f5e6a0b2d13a1c9bfe8d9a3f7b0c6d4e2a8f7a4c1d1e6e3d5f9b8a2c4a7e1f";
            var issuer = "Issuer";
            var audience = "Issuer";
            var userName = "Kisko"; // Cambia esto al usuario de prueba
            var role = "Administrador";

            return TokenHelper.GenerateToken(secretKey, issuer, audience, userName, role);
        }

        [Fact]
        public async Task GetFeedDetalle_ReturnsDetalleFeed()
        {
            // Arrange
            var idExistente = 1;
            var requestUri = $"/Feeds/Detalle/{idExistente}";
            var token = GetJwtToken(); // Obtener el token JWT

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.EnsureSuccessStatusCode(); // Verifica que la respuesta tenga un código de estado 2xx
            var responseString = await response.Content.ReadAsStringAsync();
            var feedDetalle = JsonConvert.DeserializeObject<FeedDetalleDTO>(responseString);

            // Verifica que el objeto no sea nulo
            Assert.NotNull(feedDetalle);

            Assert.NotNull(feedDetalle.Topics);

            Assert.IsType<List<string>>(feedDetalle.Topics);

            // Verifica el tipo de objeto
            Assert.IsType<FeedDetalleDTO>(feedDetalle);

            Assert.False(string.IsNullOrWhiteSpace(feedDetalle.Nombre));
        }
    }
}
