using System;
using System.Collections.Generic;

namespace OrdenesCompra.JPazos.domain.Entities
{
    public class Orden
    {
        public Guid Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public decimal Total { get; set; }

        // Relación con OrdenDetalle
        public virtual ICollection<OrdenDetalle> OrdenDetalle { get; set; } = new List<OrdenDetalle>();
    }
}
