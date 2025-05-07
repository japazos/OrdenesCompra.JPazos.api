using MapsterMapper;
using OrdenesCompra.JPazos.application.Dto.Orden;
using OrdenesCompra.JPazos.application.Exceptions;
using OrdenesCompra.JPazos.application.IServices;
using OrdenesCompra.JPazos.domain.Entities;
using OrdenesCompra.JPazos.domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrdenesCompra.JPazos.application.Services
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository ordenRepository, IMapper mapper)
        {
            _ordenRepository = ordenRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrden(OrdenCreateDto ordenDto)
        {
            // Validar duplicados antes de crear
            if (await ExisteOrdenDuplicada(ordenDto.Cliente, DateTime.Now.Date))
            {
                throw new BusinessException($"Ya existe una orden registrada para el cliente '{ordenDto.Cliente}' en la fecha actual.");
            }

            var orden = _mapper.Map<Orden>(ordenDto);

            // Calcular el total basado en los detalles
            orden.Total = orden.OrdenDetalle.Sum(d => d.Cantidad * d.PrecioUnitario);
            
            // Asignar la fecha de creación
            orden.FechaCreacion = DateTime.Now;

             _ordenRepository.Add(orden);
            await _ordenRepository.Save();
            return true;
        }

        public async Task<bool> UpdateOrden(Guid id, OrdenCreateDto ordenDto)
        {
            var orden = _ordenRepository
                .Query(
                    filter: o => o.Id == id,
                    includeProperties: "OrdenDetalle" 
                )
                .FirstOrDefault();

            if (orden == null)
                throw new BusinessException("Orden no encontrada.");

            // Validar duplicados antes de actualizar (excluyendo la orden actual)
            if (await ExisteOrdenDuplicada(ordenDto.Cliente, DateTime.Now.Date, id))
            {
                throw new BusinessException($"Ya existe otra orden registrada para el cliente '{ordenDto.Cliente}' en la fecha actual.");
            }

            // Actualizar los detalles de la orden
            var productosEnviado = ordenDto.OrdenDetalle.Select(d => d.Producto).ToList();

            // Actualizar o agregar detalles
            foreach (var detalleDto in ordenDto.OrdenDetalle)
            {
                var detalleExistente = orden.OrdenDetalle.FirstOrDefault(d => d.Producto == detalleDto.Producto);
                if (detalleExistente != null)
                {
                    detalleExistente.Cantidad = detalleDto.Cantidad;
                    detalleExistente.PrecioUnitario = detalleDto.PrecioUnitario;
                }
                else
                {
                    orden.OrdenDetalle.Add(new OrdenDetalle
                    {
                        Producto = detalleDto.Producto,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnitario = detalleDto.PrecioUnitario
                    });
                }
            }

            // Convertir a lista para usar RemoveAll para quitar los productos que no estan y actualizar la colección
            var detallesLista = orden.OrdenDetalle.ToList();
            detallesLista.RemoveAll(d => !productosEnviado.Contains(d.Producto));
            orden.OrdenDetalle = detallesLista;

            // Recalcular el total
            orden.Total = orden.OrdenDetalle.Sum(d => d.Cantidad * d.PrecioUnitario);

            // Actualizar otros campos de la orden
            _mapper.Map(ordenDto, orden);

            // Guardar los cambios
            _ordenRepository.Update(orden);
            await _ordenRepository.Save();
            return true;
        }


        public async Task<bool> DeleteOrden(Guid id)
        {
            var orden = await _ordenRepository.Find(o => o.Id == id);

            if (orden == null)
                throw new BusinessException("Orden no encontrada.");

            _ordenRepository.Remove(orden);
            await _ordenRepository.Save();
            return true;
        }

        public async Task<OrdenDetailDto?> GetOrdenById(Guid id)
        {
            var orden = _ordenRepository
                .Query(
                    filter: o => o.Id == id,
                    includeProperties: "OrdenDetalle"
                )
                .FirstOrDefault();
            return orden == null ? null : _mapper.Map<OrdenDetailDto>(orden);
        }

        public async Task<PaginatedListDto<OrdenListDto>> ListOrdenes(
            string? cliente,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? ordenarPor,
            bool ascendente,
            int pagina,
            int tamanoPagina)
        {
            IQueryable<Orden> ordenesQuery = _ordenRepository.Query(
                filter: o =>
                    (string.IsNullOrEmpty(cliente) || o.Cliente.Contains(cliente)) &&
                    (!fechaInicio.HasValue || o.FechaCreacion >= fechaInicio.Value) &&
                    (!fechaFin.HasValue || o.FechaCreacion <= fechaFin.Value),
                orderBy: ordenarPor switch
                {
                    "Cliente" => ascendente ? q => q.OrderBy(o => o.Cliente) : q => q.OrderByDescending(o => o.Cliente),
                    "FechaCreacion" => ascendente ? q => q.OrderBy(o => o.FechaCreacion) : q => q.OrderByDescending(o => o.FechaCreacion),
                    _ => q => q.OrderBy(o => o.Id)
                }
            );

            var ordenesPaginados = await ordenesQuery
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            var totalOrdenes = await _ordenRepository.Query(
                filter: o =>
                    (string.IsNullOrEmpty(cliente) || o.Cliente.Contains(cliente)) &&
                    (!fechaInicio.HasValue || o.FechaCreacion >= fechaInicio.Value) &&
                    (!fechaFin.HasValue || o.FechaCreacion <= fechaFin.Value)
            ).CountAsync();

            return new PaginatedListDto<OrdenListDto>
            {
                Items = _mapper.Map<ICollection<OrdenListDto>>(ordenesPaginados).ToList(),
                TotalItems = totalOrdenes,
                PageSize = tamanoPagina,
                CurrentPage = pagina
            };
        }
        private async Task<bool> ExisteOrdenDuplicada(string cliente, DateTime fecha, Guid? excludeId = null)
        {
            return await _ordenRepository.Query(
                o => o.Cliente == cliente && o.FechaCreacion.Date == fecha && o.Id != (excludeId ?? Guid.Empty)
            ).AnyAsync();
        }

    }
}