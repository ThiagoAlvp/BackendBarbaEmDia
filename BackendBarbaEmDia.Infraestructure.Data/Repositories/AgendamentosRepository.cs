using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;
using BackendBarbaEmDia.Infraestructure.Data.Repositories;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public class AgendamentosRepository : Repository<Agendamento, BarbeariaContext>, IAgendamentosRepository
    {
        public AgendamentosRepository(BarbeariaContext context) : base(context)
        {
        }
    }
}
