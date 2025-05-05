using MapsterMapper;
using OrdenesCompra.JPazos.application.Dto.Usuario;
using OrdenesCompra.JPazos.application.Exceptions;
using OrdenesCompra.JPazos.application.IServices;
using OrdenesCompra.JPazos.domain.Entities;
using OrdenesCompra.JPazos.domain.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdenesCompra.JPazos.application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<bool> Add(AddUsuarioDto addUsuarioDto)
        {
            bool result;
            var usuarioExists = await _usuarioRepository.List(w => w.Email.Equals(addUsuarioDto.Email));
            if (usuarioExists.Any())
            {
                throw new BusinessException("El correo ya existe, prueba con otro.");
            }

            var usuario = _mapper.Map<Usuario>(addUsuarioDto);
            usuario.Activo = true;

            _usuarioRepository.Add(usuario);

            await _usuarioRepository.Save();
            result = true;

            return result;
        }

        public async Task<ICollection<UsuarioDto>> GetAll()
        {
            var usuarios = await _usuarioRepository.List();
            return _mapper.Map<ICollection<UsuarioDto>>(usuarios);
        }
    }
}
