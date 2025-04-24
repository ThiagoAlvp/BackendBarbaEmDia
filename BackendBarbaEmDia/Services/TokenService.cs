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

        public void PreencherTokenCliente(LoginClienteResponse loginClienteResponse)
        {
            loginClienteResponse.Token =
                GerarToken(
                    _appSettings.SecretCliente, 
                    new Claim(ClaimTypes.NameIdentifier, loginClienteResponse.Cliente.Id.ToString()),
                    new Claim(ClaimTypes.Name, loginClienteResponse.Cliente.Nome ?? ""),
                    new Claim(ClaimTypes.MobilePhone, loginClienteResponse.Cliente.Telefone ?? "")
                );
        }

        public void PreencherTokenAdministrador(LoginAdministradorResponse loginAdministradorResponse)
        {
            loginAdministradorResponse.Token =
                GerarToken(
                    _appSettings.SecretAdministrador,
                    new Claim(ClaimTypes.NameIdentifier, loginAdministradorResponse.Administrador.Id.ToString()),
                    new Claim(ClaimTypes.Name, loginAdministradorResponse.Administrador.Nome ?? ""),
                    new Claim(ClaimTypes.Email, loginAdministradorResponse.Administrador.Username ?? ""),
                    new Claim(ClaimTypes.MobilePhone, loginAdministradorResponse.Administrador.Telefone ?? "")
                );
        }

        private string GerarToken(string key, params Claim[] claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
