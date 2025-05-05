using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdenesCompra.JPazos.application.Dto.Usuario;
using OrdenesCompra.JPazos.application.IServices;

namespace OrdenesCompra.JPazos.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Lista los usuarios ingresados
        /// </summary>
        [HttpGet("getUsuarios")]
        [ProducesResponseType(typeof(ICollection<UsuarioDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuariosDto = await _usuarioService.GetAll();

            if (usuariosDto.Any())
            {
                return Ok(new { success = true, data = usuariosDto, message = string.Empty });
            }

            return NotFound(new { success = false, message = "No se encontraron registros." });
        }

    }
}
