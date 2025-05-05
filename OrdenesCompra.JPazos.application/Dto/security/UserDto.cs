using OrdenesCompra.JPazos.application.Dto.Usuario;

namespace OrdenesCompra.JPazos.application.dto.security
{
    public class UserDto : UsuarioDto
    {
        public string Token { get; set; } = null!;
    }
}
