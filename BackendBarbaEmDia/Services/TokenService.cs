using BackendBarbaEmDia.Classes;
using BackendBarbaEmDia.Domain.Models.Responses;
using BackendBarbaEmDia.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendBarbaEmDia.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public void PreencherToken(LoginClienteResponse loginClienteResponse)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.SecretCliente);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, loginClienteResponse.Cliente.Id.ToString()),
                    new Claim(ClaimTypes.Name, loginClienteResponse.Cliente.Nome ?? string.Empty),
                    new Claim(ClaimTypes.MobilePhone, loginClienteResponse.Cliente.Telefone ?? string.Empty)
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            loginClienteResponse.Token = tokenHandler.WriteToken(token);
        }
    }
}
