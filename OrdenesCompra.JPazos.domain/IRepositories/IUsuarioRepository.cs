using OrdenesCompra.JPazos.domain.Entities;
using System.Threading.Tasks;

namespace OrdenesCompra.JPazos.domain.IRepositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> GetByEmail(string email);
    }
}