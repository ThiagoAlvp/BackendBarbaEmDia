using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IServicoService
    {
        Task<ServiceResult<List<ServicoResponse>>> GetAllAsync();
        Task<ServiceResult<List<ServicoResponse>>> GetAllAtivosAsync();
        Task<ServiceResult<ServicoResponse>> GetByIdAsync(int id);
        Task<ServiceResult> CreateAsync(AddUpdateServicoRequest servico);
        Task<ServiceResult> UpdateAsync(AddUpdateServicoRequest servico, int id);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> AtivarAsync(int id);
        Task<ServiceResult> InativarAsync(int id);
    }
}
