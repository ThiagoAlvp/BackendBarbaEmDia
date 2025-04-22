using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ServiceResult<LoginClienteResponse>> VerificaClienteLogin(LoginClienteRequest request);
    }
}
