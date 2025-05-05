using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdenesCompra.JPazos.application.dto.security;
using OrdenesCompra.JPazos.application.generics;
using OrdenesCompra.JPazos.application.IServices;

namespace OrdenesCompra.JPazos.api.api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [Produces("application/json")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService) 
        {
            _securityService = securityService;
        }


        /// <summary>
        /// Crea un nuevo usuario, para luego usar SingIn para que devuelva el token 
        /// </summary>
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var signUpResponse = await _securityService.SignUp(signUpDto);
            return Ok(ApiResponseFactory.SuccessWithoutData(signUpResponse, "Los datos se registraron correctamente."));
        }

        /// <summary>
        /// Valida email y password de usuario, si es correcto, devuelve el token para ser usado en Orden y Usuario 
        /// </summary>
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            var signInResponse = await _securityService.SignIn(signInDto);
            return Ok(ApiResponseFactory.SuccessWithData(signInResponse));
        }
    }
}
