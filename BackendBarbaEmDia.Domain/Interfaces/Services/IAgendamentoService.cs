using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IAgendamentoService
    {
        Task<ServiceResult> Agendar(AddUpdateAgendamentoRequest agendamento);
        Task<ServiceResult> Cancelar(int id);
        Task<ServiceResult> Reagendar(int id, AddUpdateAgendamentoRequest agendamento);
        Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentos(int? barbeiroId = null, int? clienteId = null, DateTime? data = null);
        Task<ServiceResult<AgendamentoResponse>> ObterAgendamentoPorId(int id);
        Task<ServiceResult<List<DateTime>>> ObterHorariosIndisponiveis(int idServico, int? idBarbeiro = null, TimeSpan? horarioPreferencialInicial = null, TimeSpan? horarioPreferencialFinal = null);
    }
}
