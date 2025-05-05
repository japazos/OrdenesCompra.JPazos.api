using OrdenesCompra.JPazos.application.dto.security;
using System.Threading.Tasks;

namespace OrdenesCompra.JPazos.application.IServices
{
    public interface ISecurityService
    {
        Task<bool> SignUp(SignUpDto signUpDto);
        Task<UserDto> SignIn(SignInDto signInDto);
        Task<UserDto> GetUserByEmail(string email);
    }
}
