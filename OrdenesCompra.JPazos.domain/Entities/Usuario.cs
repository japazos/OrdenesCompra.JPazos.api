using System;
using System.Collections.Generic;

namespace OrdenesCompra.JPazos.domain.Entities
{
    public partial class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
