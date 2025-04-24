using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;
using BackendBarbaEmDia.Infraestructure.Data.Repositories;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public class ServicoRepository : Repository<Servico, BarbeariaContext>, IServicoRepository
    {
        public ServicoRepository(BarbeariaContext context) : base(context)
        {
        }
    }
}
