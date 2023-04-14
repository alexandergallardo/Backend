using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _ssKey;
        public TokenService(IConfiguration config)
        {
            _ssKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.NombreUsuario)
            };

            var credenciales = new SigningCredentials(_ssKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.UtcNow.AddHours(1),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}