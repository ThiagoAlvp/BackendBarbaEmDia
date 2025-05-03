using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IBarbeiroServicoService
    {
        Task<ServiceResult> CreateBarbeiroServicoAsync(AddUpdateBarbeiroServicoRequest barbeiroServico);
        Task<ServiceResult> UpdateBarbeiroServicoAsync(AddUpdateBarbeiroServicoRequest barbeiroServico, int id);
        Task<ServiceResult> DeleteBarbeiroServicoAsync(int barbeiroId, int servicoId);
    }
}
