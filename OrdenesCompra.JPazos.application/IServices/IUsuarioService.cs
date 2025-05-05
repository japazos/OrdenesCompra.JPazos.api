using OrdenesCompra.JPazos.application.Dto.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesCompra.JPazos.application.IServices
{
    public interface IUsuarioService
    {
        Task<ICollection<UsuarioDto>> GetAll();
        Task<bool> Add(AddUsuarioDto addUsuarioDto);
    }
}
