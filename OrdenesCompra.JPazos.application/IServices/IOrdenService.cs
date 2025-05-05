using OrdenesCompra.JPazos.application.Dto.Orden;
using System.Threading.Tasks;
using System;

namespace OrdenesCompra.JPazos.application.IServices
{
    public interface IOrdenService
    {
        Task<bool> CreateOrden(OrdenCreateDto ordenDto);
        Task<bool> UpdateOrden(Guid id, OrdenCreateDto ordenDto);
        Task<bool> DeleteOrden(Guid id);
        Task<OrdenDetailDto?> GetOrdenById(Guid id);
        Task<PaginatedListDto<OrdenListDto>> ListOrdenes(
            string? cliente,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? ordenarPor,
            bool ascendente,
            int pagina,
            int tamanoPagina);
    }
}

