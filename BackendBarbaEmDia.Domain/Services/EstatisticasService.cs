using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Responses;
using System.Globalization;

namespace BackendBarbaEmDia.Domain.Services
{
    public class EstatisticasService : IEstatisticasService
    {
        private readonly IAgendamentosRepository _agendamentosRepository;

        public EstatisticasService(IAgendamentosRepository agendamentosRepository)
        {
            _agendamentosRepository = agendamentosRepository;
        }

        public async Task<ServiceResult<ResumoMesResponse>> GetResumoMes()
        {
            try
            {
                List<Agendamento> agendamentosMes = await _agendamentosRepository
                    .GetListWithIncludesAsync(
                        x => x.DataHoraInicio.Month == DateTime.Now.Month &&
                             x.DataHoraInicio.Year == DateTime.Now.Year,
                        x => x.Servico);

                int qtdeAgendamentos = agendamentosMes.Where(x => x.Status != "Cancelado").Count();

                int qtdeAgendamentosCancelados = agendamentosMes.Where(x => x.Status == "Cancelado").Count();

                decimal valorTotalAgendamentos =
                    agendamentosMes
                    .Where(x => x.Status != "Cancelado" &&
                                x.DataHoraInicio <= DateTime.Now
                    )
                    .Sum(x => x.Servico?.Preco ?? 0);

                ResumoMesResponse resumoMesResponse = new(
                    DateTime.Now.ToString("MMMM", new CultureInfo("pt-BR")),
                    qtdeAgendamentos,
                    qtdeAgendamentosCancelados,
                    valorTotalAgendamentos
                );

                return new(resumoMesResponse);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao buscar o resumo do mês: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<TopBarbeirosResponse>>> GetTopBarbeirosAgendamentos()
        {
            try
            {
                List<Agendamento> agendamentos = await _agendamentosRepository
                    .GetListWithIncludesAsync(
                        x => x.DataHoraInicio.Month == DateTime.Now.Month &&
                             x.DataHoraInicio.Year == DateTime.Now.Year &&
                             x.Status != "Cancelado",
                        x => x.Barbeiro
                    );

                List<TopBarbeirosResponse> barbeiros = agendamentos
                    .Where(x => x.Barbeiro is not null)
                    .GroupBy(x => x.Barbeiro)
                    .Select(x => new TopBarbeirosResponse(
                        x.Key!.Id,
                        x.Key.Nome,
                        x.Count()
                    ))
                    .OrderByDescending(x => x.TotalAgendamentos)
                    .ToList();

                return new(barbeiros);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao buscar os barbeiros com mais agendamentos: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<TopDiasSemanaResponse>>> GetTopDiasSemanaAgendamentos()
        {
            try
            {
                List<Agendamento> agendamentos = await _agendamentosRepository
                    .GetListAsync(
                        x => x.DataHoraInicio.Month == DateTime.Now.Month &&
                             x.DataHoraInicio.Year == DateTime.Now.Year &&
                             x.Status != "Cancelado"
                    );

                List<TopDiasSemanaResponse> diasSemana = agendamentos
                    .GroupBy(x => x.DataHoraInicio.ToString("dddd", new CultureInfo("pt-BR")))
                    .Select(x => new TopDiasSemanaResponse(
                        x.Key,
                        x.Count()
                    ))
                    .OrderByDescending(x => x.TotalAgendamentos)
                    .ToList();

                return new(diasSemana);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao buscar os dias da semana com mais agendamentos: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<TopHorariosResponse>>> GetTopHorariosAgendamentos()
        {
            try
            {
                List<Agendamento> agendamentos = await _agendamentosRepository
                    .GetListAsync(
                        x => x.DataHoraInicio.Month == DateTime.Now.Month &&
                             x.DataHoraInicio.Year == DateTime.Now.Year &&
                             x.Status != "Cancelado"
                    );

                List<TopHorariosResponse> horarios = agendamentos
                    .GroupBy(x => x.DataHoraInicio.TimeOfDay)
                    .Select(x => new TopHorariosResponse(
                        x.Key,
                        x.Count()
                    ))
                    .OrderByDescending(x => x.TotalAgendamentos)
                    .ToList();

                return new(horarios);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao buscar os horários com mais agendamentos: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<TopServicosResponse>>> GetTopServicosAgendamentos()
        {
            try
            {
                List<Agendamento> agendamentos = await _agendamentosRepository
                    .GetListWithIncludesAsync(
                        x => x.DataHoraInicio.Month == DateTime.Now.Month &&
                             x.DataHoraInicio.Year == DateTime.Now.Year &&
                             x.Status != "Cancelado",
                        x => x.Servico
                    );

                List<TopServicosResponse> servicos = agendamentos
                    .Where(x => x.Servico is not null)
                    .GroupBy(x => x.Servico)
                    .Select(x => new TopServicosResponse(
                        x.Key!.Id,
                        x.Key.Descricao,
                        x.Count()
                    ))
                    .OrderByDescending(x => x.TotalAgendamentos)
                    .ToList();

                return new(servicos);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao buscar os serviços com mais agendamentos: {ex.GetFullMessage()}", true);
            }
        }
    }
}
