using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;

namespace BackendBarbaEmDia.Infraestructure.Data.Repositories
{
    public class ParametrizacaoHorarioFuncionamentoRepository : Repository<ParametrizacaoHorarioFuncionamento, BarbeariaContext>, IParametrizacaoHorarioFuncionamentoRepository
    {
        public ParametrizacaoHorarioFuncionamentoRepository(BarbeariaContext context) : base(context)
        {
        }
    }
}
