using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface ITravamentoService
    {
        Task<ServiceResult> Criar(AddUpdateTravamentoRequest travamento);
        Task<ServiceResult> Atualizar(int id, AddUpdateTravamentoRequest travamento);
        Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentos();
        Task<ServiceResult<TravamentoResponse>> ObterTravamentoPorId(int id);
        Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentosPorBarbeiroId(int barbeiroId);
        Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentosPorData(DateTime data);
        Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentosPorDataEBarbeiro(DateTime data, int barbeiroId);
        Task<ServiceResult> Deletar(int id);
    }
}
