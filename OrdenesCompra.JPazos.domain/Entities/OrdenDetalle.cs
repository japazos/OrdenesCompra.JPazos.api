using System;

namespace OrdenesCompra.JPazos.domain.Entities
{
    public class OrdenDetalle
    {
        public Guid Id { get; set; }
        public Guid OrdenId { get; set; }
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        // Propiedad de navegación hacia Orden
        public Orden Orden { get; set; } = null!;
    }
}
