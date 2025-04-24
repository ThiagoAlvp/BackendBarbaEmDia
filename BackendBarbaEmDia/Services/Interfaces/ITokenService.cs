using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Services.Interfaces
{
    public interface ITokenService
    {
        void PreencherTokenAdministrador(LoginAdministradorResponse loginAdministradorResponse);
        void PreencherTokenCliente(LoginClienteResponse loginClienteResponse);
    }
}
