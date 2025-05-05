using System;

namespace OrdenesCompra.JPazos.application.Dto.Usuario
{
    public class UsuarioDto
    {
        public Guid UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Activo { get; set; }
        public string FechaCreacion { get; set; } = null!;
    }
}
