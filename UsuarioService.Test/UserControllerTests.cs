using UsuarioService.Infrastructure.DTOs;
using Newtonsoft.Json;

namespace UsuarioService.Test
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public UserControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetUsers_ReturnsUser()
        {
            // Arrange
            var requestUri = "/Usuario"; 

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.EnsureSuccessStatusCode(); 
            var responseString = await response.Content.ReadAsStringAsync();
            var usuarios = JsonConvert.DeserializeObject<List<UsuarioDTO>>(responseString);

            Assert.NotNull(usuarios);

            Assert.NotEmpty(usuarios);

            Assert.IsType<List<UsuarioDTO>>(usuarios);

            // Verifica propiedades individuales del primer usuario (si existe)
            if (usuarios.Count > 0)
            {
                var usuario = usuarios[0];
                Assert.NotNull(usuario); 
                Assert.NotNull(usuario.Username); 
                Assert.False(string.IsNullOrWhiteSpace(usuario.Username)); 
            }
        }
    }
}
