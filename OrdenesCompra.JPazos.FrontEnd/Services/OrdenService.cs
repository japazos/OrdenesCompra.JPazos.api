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

        public async Task<List<OrdenDto>> GetOrdenes()
        {
            return await _httpClient.GetFromJsonAsync<List<OrdenDto>>("api/Orden/list");
        }

        public async Task<bool> CreateOrden(OrdenDto orden)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Orden/create", orden);
            return response.IsSuccessStatusCode;
        }
    }
}
