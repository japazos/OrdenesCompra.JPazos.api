using Microsoft.JSInterop;
using OrdenesCompra.JPazos.FrontEnd.Dto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


namespace OrdenesCompra.JPazos.FrontEnd.Services
{
    public class SecurityService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public SecurityService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }


public async Task<SignInResponse> SignIn(SignInDto signInDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Security/signIn", signInDto);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                // Deserializa la estructura completa del API
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<UserDto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse?.Data != null && !string.IsNullOrEmpty(apiResponse.Data.Token))
                {
                    var token = apiResponse.Data.Token;
                    // Guarda el token en localStorage
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
                    return new SignInResponse { Success = true, Message = "Inicio de sesión exitoso." };
                }
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return new SignInResponse { Success = false, Message = errorMessage };
        }
        catch (Exception ex)
        {
            return new SignInResponse { Success = false, Message = $"Error: {ex.Message}" };
        }
    }



    public async Task<bool> SignUp(SignUpDto signUpDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Security/signUp", signUpDto);
            return response.IsSuccessStatusCode;
        }
    }
}

public class SignInResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

