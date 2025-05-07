using Microsoft.JSInterop;
using OrdenesCompra.JPazos.FrontEnd.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace OrdenesCompra.JPazos.FrontEnd.Services
{
    public class OrdenService
    {
        private readonly HttpClient _httpClient;

        public OrdenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ApiResponse<PaginatedListDto<OrdenListDto>>> ListOrdenesAsync(
            string? cliente = null,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null,
            string ordenarPor = "FechaCreacion",
            bool ascendente = true,
            int pagina = 1,
            int tamanoPagina = 10,
            string? token = null
            )
        {
            var fechaInicioStr = fechaInicio?.ToString("yyyy-MM-dd") ?? "";
            var fechaFinStr = fechaFin?.ToString("yyyy-MM-dd") ?? "";
            var queryParams = $"?cliente={cliente}&fechaInicio={fechaInicioStr}&fechaFin={fechaFinStr}&ordenarPor={ordenarPor}&ascendente={ascendente}&pagina={pagina}&tamanoPagina={tamanoPagina}";

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Orden/list{queryParams}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<ApiResponse<PaginatedListDto<OrdenListDto>>>(jsonResponse, options);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al listar órdenes: {response.StatusCode} - {errorMessage}");
                    return new ApiResponse<PaginatedListDto<OrdenListDto>> { Success = false, Message = $"Error al listar órdenes: {response.StatusCode} - {errorMessage}" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar órdenes: {ex.Message}");
                return new ApiResponse<PaginatedListDto<OrdenListDto>> { Success = false, Message = $"Error al listar órdenes: {ex.Message}" };
            }
        }

        public async Task<List<OrdenDetailDto>> GetOrdenesAsync(string? token = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _httpClient.GetFromJsonAsync<List<OrdenDetailDto>>("api/Orden/list");
        }

        public async Task<bool> CreateOrdenAsync(OrdenCreateDto orden, string? token = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("api/Orden/create", orden);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrdenByIdAsync(string ordenId, string? token = null)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Orden/delete/{ordenId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la orden con ID {ordenId}: {ex.Message}");
                return false;
            }
        }
    }
}