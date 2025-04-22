using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Services.Interfaces
{
    public interface ITokenService
    {
        void PreencherToken(LoginClienteResponse loginClienteResponse);
    }
}
