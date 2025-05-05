using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdenesCompra.JPazos.application.Dto.Orden;
using OrdenesCompra.JPazos.application.generics;
using OrdenesCompra.JPazos.application.IServices;

namespace OrdenesCompra.JPazos.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenService _ordenService;

        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        /// <summary>
        /// Crea una orden: Recibe una orden con sus detalles y la almacena en la base de datos.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrden([FromBody] OrdenCreateDto ordenDto)
        {
            var createOrdenResponse = await _ordenService.CreateOrden(ordenDto);
            return Ok(ApiResponseFactory.SuccessWithoutData(createOrdenResponse, "Los datos se registraron correctamente."));
        }

        /// <summary>
        /// Actualiza una orden: Permite modificar los datos de la orden y sus detalles.
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrden(Guid id, [FromBody] OrdenCreateDto ordenDto)
        {
            var updateOrdenResponse = await _ordenService.UpdateOrden(id, ordenDto);
            return Ok(ApiResponseFactory.SuccessWithoutData(updateOrdenResponse, "Los datos se actualizaron correctamente."));
        }

        /// <summary>
        /// Elimina una orden: Elimina la orden y sus detalles asociados.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrden(Guid id)
        {
            var deleteOrdenResponse = await _ordenService.DeleteOrden(id);
            return Ok(ApiResponseFactory.SuccessWithoutData(deleteOrdenResponse, "Orden eliminada correctamente."));
        }

        /// <summary>
        /// Obtiene una orden por ID: Devuelve los datos de la orden junto con sus detalles.
        /// </summary>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetOrdenById(Guid id)
        {
            var ordenDto = await _ordenService.GetOrdenById(id);
            return ordenDto != null
                ? Ok(new { success = true, data = ordenDto })
                : NotFound(new { success = false, message = "Orden no encontrada." });
        }

        /// <summary>
        /// Lista órdenes: paginación, filtros por cliente y rango de fechas, ordena los resultados.
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> ListOrdenes(
            [FromQuery] string? cliente,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] string? ordenarPor = "FechaCreacion",
            [FromQuery] bool ascendente = true,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanoPagina = 10)
        {
            var ordenes = await _ordenService.ListOrdenes(cliente, fechaInicio, fechaFin, ordenarPor, ascendente, pagina, tamanoPagina);
            return ordenes.Items.Any()
                ? Ok(new { success = true, data = ordenes })
                : NotFound(new { success = false, message = "No se encontraron órdenes." });
        }
    }
}

