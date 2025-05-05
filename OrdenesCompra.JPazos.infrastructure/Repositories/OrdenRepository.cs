using OrdenesCompra.JPazos.domain.Entities;
using OrdenesCompra.JPazos.domain.IRepositories;
using OrdenesCompra.JPazos.infrastructure.Context;

namespace OrdenesCompra.JPazos.infrastructure.Repositories
{
    public class OrdenRepository : GenericRepository<Orden>, IOrdenRepository
    {
        public OrdenRepository(OrdenesCompraContext context) : base(context) { }
    }
}