using MapsterMapper;
using OrdenesCompra.JPazos.domain.IRepositories;
using OrdenesCompra.JPazos.application.dto.security;
using OrdenesCompra.JPazos.application.Exceptions;
using OrdenesCompra.JPazos.application.hashing;
using OrdenesCompra.JPazos.application.IServices;

using System.Linq;
using System.Threading.Tasks;
using OrdenesCompra.JPazos.domain.Entities;

namespace OrdenesCompra.JPazos.application.services
{
    public class SecurityService : ISecurityService
    {
        private readonly IJwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public SecurityService(IUsuarioRepository usuarioRepository, IMapper mapper, IJwtService jwtService)
        {
            _jwtService = jwtService;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<bool> SignUp(SignUpDto signUpDto)
        {
            bool result;
            var usuarioExists = await _usuarioRepository.List(w => w.Email.Equals(signUpDto.Email));
            if (usuarioExists.Any())
            {
                throw new BusinessException("El mail ya existe, prueba con otro.");
            }

            var usuario = _mapper.Map<Usuario>(signUpDto);
            usuario.Password = BcryptHashing.Hash(signUpDto.Password);
            usuario.Activo = true;
                
            _usuarioRepository.Add(usuario);
            await _usuarioRepository.Save();

            result = true;

            return result;
        }

        public async Task<UserDto> SignIn(SignInDto signInDto)
        {
            var customer = await _usuarioRepository.GetByEmail(signInDto.Email);
            if(customer is null || !BcryptHashing.Verify(signInDto.Password, customer.Password)) 
            {
                throw new BusinessException("Usuario y/o contraseña incorrecto.");
            }

            var userDto = _mapper.Map<UserDto>(customer);
            userDto.Token = _jwtService.GenerateToken(userDto.UsuarioId.ToString(), userDto.Email, "ROLE_ADMIN");
            return userDto;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var customer = await _usuarioRepository.GetByEmail(email);
            var userDto = _mapper.Map<UserDto>(customer!);
            return userDto;
        }
    }
}
