
namespace OrdenesCompra.JPazos.FrontEnd.Dto
{
    // DTO para crear una orden
    public class OrdenCreateDto
    {
        public string Cliente { get; set; } = string.Empty;
        public ICollection<OrdenDetalleDto> OrdenDetalle { get; set; } = new List<OrdenDetalleDto>();
    }

    // DTO para obtener los detalles de una orden
    public class OrdenDetailDto
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrdenDetalleDto> OrdenDetalle { get; set; } = new List<OrdenDetalleDto>();
    }

    // DTO para listar órdenes con paginación
    public class OrdenListDto
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
    }

    // DTO para los detalles de una orden
    public class OrdenDetalleDto
    {
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    // DTO para manejar paginación
    public class PaginatedListDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}


