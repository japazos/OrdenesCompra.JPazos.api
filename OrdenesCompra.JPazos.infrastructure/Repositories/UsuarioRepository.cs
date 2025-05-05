using Microsoft.EntityFrameworkCore;
using OrdenesCompra.JPazos.domain.Entities;
using OrdenesCompra.JPazos.domain.IRepositories;
using OrdenesCompra.JPazos.infrastructure.Context;
using System.Threading.Tasks;

namespace OrdenesCompra.JPazos.infrastructure.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(OrdenesCompraContext context) : base(context) { }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(w => w.Email.Equals(email));
        }
    }
}
