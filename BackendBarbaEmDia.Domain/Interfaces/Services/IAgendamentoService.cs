using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IAgendamentoService
    {
        Task<ServiceResult> Agendar(AddUpdateAgendamento agendamento);
        Task<ServiceResult> Cancelar(int id);
        Task<ServiceResult> Reagendar(int id, AddUpdateAgendamento agendamento);
        Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentos();
        Task<ServiceResult<AgendamentoResponse>> ObterAgendamentoPorId(int id);
        Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentosPorClienteId(int clienteId);
        Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentosPorBarbeiroId(int barbeiroId);
        Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentosPorData(DateTime data);
    }
}
