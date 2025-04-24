using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Services
{
    public class ParametrizacaoHorarioFuncionamentoService : IParametrizacaoHorarioFuncionamentoService
    {
        private readonly IParametrizacaoHorarioFuncionamentoRepository _parametrizacaoHorarioFuncionamentoRepository;

        public ParametrizacaoHorarioFuncionamentoService(IParametrizacaoHorarioFuncionamentoRepository parametrizacaoHorarioFuncionamentoRepository)
        {
            _parametrizacaoHorarioFuncionamentoRepository = parametrizacaoHorarioFuncionamentoRepository;
        }

        public async Task<ServiceResult<List<ParametrizacaoHorarioFuncionamento>>> GetParametrizacaoHorarios()
        {
            try
            {
                return new(await _parametrizacaoHorarioFuncionamentoRepository.GetListAsync());
            }
            catch (Exception ex)
            {   
                return new(false, "Erro ao obter os horários de funcionamento: " + ex.GetFullMessage(), true);
            }
        }
    }
}
