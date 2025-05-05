using System.Net.Http.Json;

namespace OrdenesCompra.JPazos.FrontEnd.Services
{
    public class SecurityService
    {
        private readonly HttpClient _httpClient;

        public SecurityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SignIn(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Security/signIn", new { email, password });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SignUp(string nombres, string apellidos, string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Security/signUp", new { nombres, apellidos, email, password });
            return response.IsSuccessStatusCode;
        }
    }
}
