using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using System.Linq.Expressions;

namespace BackendBarbaEmDia.Domain.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentosRepository _agendamentoRepository;
        private readonly ITravamentosRepository _travamentoRepository;
        private readonly IParametrizacaoHorarioFuncionamentoRepository _parametrizacaoHorarioFuncionamentoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IBarbeiroServicoRepository _barbeiroServicoRepository;

        public AgendamentoService(
            IAgendamentosRepository agendamentoRepository,
            ITravamentosRepository travamentoRepository,
            IParametrizacaoHorarioFuncionamentoRepository parametrizacaoHorarioFuncionamentoRepository,
            IServicoRepository servicoRepository,
            IBarbeiroServicoRepository barbeiroServicoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _travamentoRepository = travamentoRepository;
            _parametrizacaoHorarioFuncionamentoRepository = parametrizacaoHorarioFuncionamentoRepository;
            _servicoRepository = servicoRepository;
            _barbeiroServicoRepository = barbeiroServicoRepository;
        }

        public async Task<ServiceResult> Agendar(AddUpdateAgendamentoRequest agendamento)
        {
            try
            {
                ServiceResult resultVerifica = await VerificarAgendamento(agendamento);

                if (!resultVerifica.Success)
                    return resultVerifica;

                Agendamento agendamentoDb = new()
                {
                    IdBarbeiro = agendamento.IdBarbeiro ?? 0,
                    IdCliente = agendamento.IdCliente,
                    IdServico = agendamento.IdServico,
                    DataHoraInicio = agendamento.DataHoraInicio,
                    Duracao = agendamento.Duracao!.Value,
                    Status = "Agendado",
                };

                await _agendamentoRepository.AddAsync(agendamentoDb);

                return new("Agendamento realizado com sucesso");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao agendar: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> Reagendar(int id, AddUpdateAgendamentoRequest agendamento)
        {
            try
            {
                ServiceResult resultVerifica = await VerificarAgendamento(agendamento, id);

                if (!resultVerifica.Success)
                    return resultVerifica;

                Agendamento? agendamentoDb = await _agendamentoRepository.GetByIdAsync(id);

                if (agendamentoDb is null)
                    return new(false, "Agendamento não encontrado");

                agendamentoDb.IdBarbeiro = agendamento.IdBarbeiro ?? 0;
                agendamentoDb.IdCliente = agendamento.IdCliente;
                agendamentoDb.IdServico = agendamento.IdServico;
                agendamentoDb.DataHoraInicio = agendamento.DataHoraInicio;
                agendamentoDb.Duracao = agendamento.Duracao!.Value;
                agendamentoDb.Status = "Agendado";

                await _agendamentoRepository.UpdateAsync(agendamentoDb);

                return new("Reagendamento realizado com sucesso");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao reagendar: {ex.GetFullMessage()}", true);
            }
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

        public async Task<ServiceResult<List<AgendamentoResponse>>> ListarAgendamentos(int? barbeiroId = null, int? clienteId = null, DateTime? data = null)
        {
            Expression<Func<Agendamento, bool>>? expression =
                x => (barbeiroId == null || x.IdBarbeiro == barbeiroId) &&
                     (clienteId == null || x.IdCliente == clienteId) &&
                     (data == null || x.DataHoraInicio.Date == data.Value.Date);

            return await ListarAgendamentosInternal(expression);
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

        public async Task<ServiceResult<List<DateTime>>> ObterHorariosIndisponiveis(
            int idServico,
            int? idBarbeiro = null,
            TimeSpan? horarioPreferencialInicial = null,
            TimeSpan? horarioPreferencialFinal = null
        )
        {
            try
            {
                List<DateTime> horariosIndisponiveis = new();                

                List<BarbeiroServico> barbeirosServico = await _barbeiroServicoRepository.GetBarbeirosByServicoId(idServico);

                if (idBarbeiro is not null)
                    barbeirosServico = barbeirosServico.Where(x => x.IdBarbeiro == idBarbeiro).ToList();

                if (!barbeirosServico.Any())
                    return new(false, "Nenhum barbeiro disponível para este serviço");

                foreach (var barbeiroServico in barbeirosServico)
                {
                    int idBarbeiroAtual = barbeiroServico.IdBarbeiro;

                    List<Agendamento> agendamentos = await _agendamentoRepository
                        .GetListAsync(x => x.IdBarbeiro == idBarbeiroAtual &&
                                           x.DataHoraInicio.Date >= DateTime.Now.Date &&
                                           x.Status != "Cancelado");

                    foreach (Agendamento agendamento in agendamentos)
                    {
                        DateTime dataHoraInicio = agendamento.DataHoraInicio;
                        DateTime dataHoraFim = dataHoraInicio.Add(agendamento.Duracao);

                        for (DateTime horario = dataHoraInicio; horario < dataHoraFim; horario = horario.AddMinutes(15))
                        {
                            if (horarioPreferencialInicial != null && horario.TimeOfDay < horarioPreferencialInicial)
                                continue;

                            if (horarioPreferencialFinal != null && horario.TimeOfDay > horarioPreferencialFinal)
                                continue;

                            horariosIndisponiveis.Add(horario);
                        }
                    }

                    List<Travamento> travamentos = await _travamentoRepository
                        .GetListAsync(x => x.IdBarbeiro == idBarbeiroAtual &&
                                           x.DataHoraInicio.Date >= DateTime.Now.Date);

                    foreach (Travamento travamento in travamentos)
                    {
                        DateTime dataHoraInicio = travamento.DataHoraInicio;
                        DateTime dataHoraFim = travamento.DataHoraFim;

                        for (DateTime horario = dataHoraInicio; horario < dataHoraFim; horario = horario.AddMinutes(15))
                        {
                            if (horarioPreferencialInicial != null && horario.TimeOfDay < horarioPreferencialInicial)
                                continue;

                            if (horarioPreferencialFinal != null && horario.TimeOfDay > horarioPreferencialFinal)
                                continue;

                            horariosIndisponiveis.Add(horario);
                        }
                    }
                }

                // Retorna apenas os horários que estão indisponíveis para todos os barbeiros
                var horariosIndisponiveisParaTodos = horariosIndisponiveis
                    .GroupBy(h => h)
                    .Where(g => g.Count() == barbeirosServico.Count)
                    .Select(g => g.Key)
                    .ToList();

                return new(horariosIndisponiveisParaTodos);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao obter horários indisponíveis: {ex.GetFullMessage()}", true);
            }
        }

        private async Task<ServiceResult> VerificarAgendamento(AddUpdateAgendamentoRequest agendamento, int? id = null)
        {
            ServiceResult<TimeSpan> duracaoServicoResult = await ObterDuracaoServico(agendamento.IdServico, agendamento.IdBarbeiro);

            if (!duracaoServicoResult.Success)
                return duracaoServicoResult;

            agendamento.Duracao = duracaoServicoResult.Data;

            bool horarioForaDoExpediente = await _parametrizacaoHorarioFuncionamentoRepository
                .ExistsAsync(
                    x => x.DiaSemana == agendamento.DataHoraInicio.DayOfWeek &&
                    (agendamento.DataHoraInicio.TimeOfDay < x.HoraInicio || agendamento.DataHoraInicio.TimeOfDay.Add(agendamento.Duracao.Value) > x.HoraFim)
                );

            if (horarioForaDoExpediente)
                return new(false, "Horário fora do expediente");

            if (agendamento.IdBarbeiro is null || agendamento.IdBarbeiro == 0)
            {
                ServiceResult<int> resultEncontrarBabeiro = await EncontrarBarbeiroDisponivel(agendamento);

                if (!resultEncontrarBabeiro.Success)
                    return resultEncontrarBabeiro;

                agendamento.IdBarbeiro = resultEncontrarBabeiro.Data;
            }

            duracaoServicoResult = await ObterDuracaoServico(agendamento.IdServico, agendamento.IdBarbeiro);

            if (!duracaoServicoResult.Success)
                return duracaoServicoResult;

            agendamento.Duracao = duracaoServicoResult.Data;

            bool horarioTravado = await _travamentoRepository.ExistsAsync(
                x => x.IdBarbeiro == agendamento.IdBarbeiro &&
                     agendamento.DataHoraInicio >= x.DataHoraInicio &&
                     agendamento.DataHoraInicio <= x.DataHoraFim
                );

            if (horarioTravado)
                return new(false, "Horário indisponível para agendamento");

            DateTime terminoServico = agendamento.DataHoraInicio.Add(agendamento.Duracao.Value);

            bool agendamentoExistente = await _agendamentoRepository
                .ExistsAsync(
                    x => x.IdBarbeiro == agendamento.IdBarbeiro &&
                    (id == null || x.Id != id) &&
                    (
                        (agendamento.DataHoraInicio >= x.DataHoraInicio && agendamento.DataHoraInicio < (x.DataHoraInicio + x.Duracao)) || // Início no meio de outro
                        (terminoServico > x.DataHoraInicio && terminoServico <= (x.DataHoraInicio + x.Duracao)) || // Término no meio de outro
                        (agendamento.DataHoraInicio <= x.DataHoraInicio && terminoServico >= (x.DataHoraInicio + x.Duracao)) // Engloba outro
                    )
                );

            if (agendamentoExistente)
                return new(false, "Horário já agendado");

            return new("Agendamento válido");
        }

        private async Task<ServiceResult<TimeSpan>> ObterDuracaoServico(int idServico, int? idBarbeiro = null)
        {
            Servico? servico = await _servicoRepository.GetByIdAsync(idServico);

            if (servico is null)
                return new(false, "Serviço não encontrado");

            TimeSpan duracaoServico = servico.DuracaoPadrao;

            if (idBarbeiro is null)
                return new(duracaoServico);

            BarbeiroServico? barbeiroServico = await _barbeiroServicoRepository
                .GetFirstAsync(x => x.IdBarbeiro == idBarbeiro && x.IdServico == idServico);

            if (barbeiroServico is null)
                return new(false, "Serviço não encontrado para o barbeiro");

            if (barbeiroServico.TempoPersonalizado != null && barbeiroServico.TempoPersonalizado != TimeSpan.Zero)
                duracaoServico = barbeiroServico.TempoPersonalizado.Value;

            return new(duracaoServico);
        }

        private async Task<ServiceResult<int>> EncontrarBarbeiroDisponivel(AddUpdateAgendamentoRequest agendamento)
        {
            try
            {
                // Obter a duração do serviço
                ServiceResult<TimeSpan> duracaoServicoResult = await ObterDuracaoServico(agendamento.IdServico);

                if (!duracaoServicoResult.Success)
                    return new(false, "Erro ao obter duração do serviço: " + duracaoServicoResult.Message);

                agendamento.Duracao = duracaoServicoResult.Data;

                // Obter todos os barbeiros que realizam o serviço
                List<BarbeiroServico> barbeirosServico = await _barbeiroServicoRepository.GetBarbeirosByServicoId(agendamento.IdServico);

                if (!barbeirosServico.Any())
                    return new(false, "Nenhum barbeiro disponível para este serviço");

                foreach (var barbeiroServico in barbeirosServico)
                {
                    int idBarbeiro = barbeiroServico.IdBarbeiro;

                    if (barbeiroServico.TempoPersonalizado != null && barbeiroServico.TempoPersonalizado != TimeSpan.Zero)
                        agendamento.Duracao = barbeiroServico.TempoPersonalizado.Value;

                    // Verificar se o barbeiro está disponível no horário solicitado
                    bool horarioIndisponivel = await _agendamentoRepository.ExistsAsync(
                        x => x.IdBarbeiro == idBarbeiro &&
                             (
                                 (agendamento.DataHoraInicio >= x.DataHoraInicio && agendamento.DataHoraInicio < (x.DataHoraInicio + x.Duracao)) || // Início no meio de outro
                                 (agendamento.DataHoraInicio.Add(agendamento.Duracao.Value) > x.DataHoraInicio && agendamento.DataHoraInicio.Add(agendamento.Duracao.Value) <= (x.DataHoraInicio + x.Duracao)) || // Término no meio de outro
                                 (agendamento.DataHoraInicio <= x.DataHoraInicio && agendamento.DataHoraInicio.Add(agendamento.Duracao.Value) >= (x.DataHoraInicio + x.Duracao)) // Engloba outro
                             )
                    );

                    if (horarioIndisponivel)
                        continue;

                    // Verificar se o barbeiro não está com horário travado
                    bool horarioTravado = await _travamentoRepository.ExistsAsync(
                        x => x.IdBarbeiro == idBarbeiro &&
                             agendamento.DataHoraInicio >= x.DataHoraInicio &&
                             agendamento.DataHoraInicio < x.DataHoraFim
                    );

                    if (horarioTravado)
                        continue;

                    // Se passou por todas as verificações, o barbeiro está disponível
                    return new(idBarbeiro, "Barbeiro disponível encontrado");
                }

                return new(false, "Nenhum barbeiro disponível para o horário solicitado");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao encontrar barbeiro disponível: {ex.GetFullMessage()}", true);
            }
        }
    }
}
