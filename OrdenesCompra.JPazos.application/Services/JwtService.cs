using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrdenesCompra.JPazos.application.IServices;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrdenesCompra.JPazos.application.services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public string GenerateToken(string customerId, string email, string roleId)
        {
            var issuer = _configuration.GetValue<string>("Jwt:Issuer");
            var audience = _configuration.GetValue<string>("Jwt:Audience");
            var secretKey = _configuration.GetValue<string>("Jwt:SecretKey");
            var lifetime = _configuration.GetValue<int>("Jwt:Lifetime");

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtHeader = new JwtHeader(signingCredentials);

            var claims = new[] 
            {
                new Claim(ClaimTypes.Sid, customerId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, roleId),
            };

            var payload = new JwtPayload(issuer, audience, claims, DateTime.Now, DateTime.Now.AddSeconds(lifetime));
            
            var token = new JwtSecurityToken(jwtHeader, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
