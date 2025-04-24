using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IAdministradorService
    {
        Task<ServiceResult> CreateAsync(AddUpdateAdministradorRequest administrador);
        Task<ServiceResult<List<AdministradorResponse>>> GetAllAsync();
        Task<ServiceResult<AdministradorResponse>> GetByIdAsync(int id);
        Task<ServiceResult> UpdateAsync(AddUpdateAdministradorRequest administrador, int id);
        Task<ServiceResult<LoginAdministradorResponse>> VerificaAdministradorLogin(LoginAdministradorRequest request);
    }
}
