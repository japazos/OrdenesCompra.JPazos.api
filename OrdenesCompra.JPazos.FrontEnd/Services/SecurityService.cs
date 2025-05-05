using OrdenesCompra.JPazos.FrontEnd.Dto;
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

        public async Task<bool> SignIn(SignInDto signInDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Security/signIn", signInDto);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    return true;
                }
                else {                     
                    // Manejo de errores
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

        }

        public async Task<bool> SignUp(SignUpDto signUpDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Security/signUp", signUpDto);
            return response.IsSuccessStatusCode;
        }
    }
}
