using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendBarbaEmDia.Domain.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentosRepository _agendamentoRepository;
        private readonly ITravamentosRepository _travamentoRepository;

        public AgendamentoService(IAgendamentosRepository agendamentoRepository, ITravamentosRepository travamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _travamentoRepository = travamentoRepository;
        }

        public Task<ServiceResult> Agendar(AddUpdateAgendamento agendamento)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> Reagendar(int id, AddUpdateAgendamento agendamento)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> VerificarAgendamento(AddUpdateAgendamento agendamento, int? id = null)
        {
            //await _travamentoRepository.ExistsAsync(
            //    x => agendamento.DataHoraInicio >= x.DataHoraInicio && agendamento.DataHoraInicio <= );

            return false;
        }

        public async Task<ServiceResult> Cancelar(int id)
        {
            try
            {
                Agendamento? agendamento = await _agendamentoRepository.GetByIdAsync(id);

                if (agendamento == null)
                    return new(false, "Agendamento não encontrado");

                agendamento.Status = "Cancelado";

                await _agendamentoRepository.UpdateAsync(agendamento);

                return new("Agendamento cancelado com sucesso");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao cancelar agendamento: {ex.GetFullMessage()}", true);
            }
        }

        private async Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentosInternal(Expression<Func<Agendamento, bool>>? expression = null)
        {
            try
            {
                List<Agendamento> agendamentosResponse = await
                    _agendamentoRepository.GetListWithIncludesAsync(
                        expression,
                        x => x.Cliente,
                        x => x.Barbeiro,
                        x => x.Servico
                    );

                List<AgendamentoResponse> agendamentos = [];

                agendamentos = agendamentosResponse.Select(x => new AgendamentoResponse(x)).ToList();

                return new(agendamentos);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao listar agendamentos: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentos()
        {
            return await ListarAgendamentosInternal();
        }

        public Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentosPorBarbeiroId(int barbeiroId)
        {
            return ListarAgendamentosInternal(x => x.IdBarbeiro == barbeiroId);
        }

        public Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentosPorClienteId(int clienteId)
        {
            return ListarAgendamentosInternal(x => x.IdCliente == clienteId);
        }

        public Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentosPorData(DateTime data)
        {
            return ListarAgendamentosInternal(x => x.DataHoraInicio.Date == data.Date);
        }

        public async Task<ServiceResult<AgendamentoResponse>> ObterAgendamentoPorId(int id)
        {
            try
            {
                Agendamento? agendamentoDb = await _agendamentoRepository.GetByIdAsync(id);

                if (agendamentoDb == null)
                    return new(false, "Agendamento não encontrado");

                AgendamentoResponse agendamento = new(agendamentoDb);

                return new(agendamento);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao obter agendamento: {ex.GetFullMessage()}", true);
            }
        }
    }
}
