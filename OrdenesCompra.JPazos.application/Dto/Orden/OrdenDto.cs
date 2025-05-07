using OrdenesCompra.JPazos.domain.Entities;
using System;
using System.Collections.Generic;

namespace OrdenesCompra.JPazos.application.Dto.Orden
{
    // DTO para crear una orden
    public class OrdenCreateDto
    {
        public string Cliente { get; set; } = string.Empty;
        public ICollection<OrdenCreateDetalleDto> OrdenDetalle { get; set; } = new List<OrdenCreateDetalleDto>();
    }

    // DTO para obtener los detalles de una orden
    public class OrdenDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrdenDetalleDto> OrdenDetalle { get; set; } = new List<OrdenDetalleDto>();
    }

    // DTO para listar órdenes con paginación
    public class OrdenListDto
    {
        public string Id { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
    }

    // DTO para los detalles de una orden
    public class OrdenDetalleDto
    {
        public string Id { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class OrdenCreateDetalleDto
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


