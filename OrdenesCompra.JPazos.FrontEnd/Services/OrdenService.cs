using OrdenesCompra.JPazos.FrontEnd.Dto;
using System.Net.Http.Json;

namespace OrdenesCompra.JPazos.FrontEnd.Services
{
    public class OrdenService
    {
        private readonly HttpClient _httpClient;

        public OrdenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrdenDetailDto>> GetOrdenes()
        {
            return await _httpClient.GetFromJsonAsync<List<OrdenDetailDto>>("api/Orden/list");
        }

        public async Task<bool> CreateOrden(OrdenCreateDto orden)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Orden/create", orden);
            return response.IsSuccessStatusCode;
        }
    }
}
