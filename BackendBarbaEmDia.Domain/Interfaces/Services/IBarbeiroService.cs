using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IBarbeiroService
    {
        Task<ServiceResult<List<BarbeiroResponse>>> GetAllAsync();
        Task<ServiceResult<BarbeiroResponse>> GetByIdAsync(int id);
        Task<ServiceResult> CreateAsync(AddUpdateBarbeiroRequest barbeiro);
        Task<ServiceResult> UpdateAsync(AddUpdateBarbeiroRequest barbeiro, int id);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<BarbeiroResponse>>> GetAllAtivosAsync(int? idServico = null);
        Task<ServiceResult> AtivarAsync(int id);
        Task<ServiceResult> InativarAsync(int id);
    }
}
