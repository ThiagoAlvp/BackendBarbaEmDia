using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IParametrizacaoHorarioFuncionamentoService
    {
        Task<ServiceResult<List<ParametrizacaoHorarioFuncionamento>>> GetParametrizacaoHorarios();
    }
}
