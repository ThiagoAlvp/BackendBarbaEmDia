using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;
using BackendBarbaEmDia.Infraestructure.Data.Repositories;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public class TravamentosRepository : Repository<Travamento, BarbeariaContext>, ITravamentosRepository
    {
        public TravamentosRepository(BarbeariaContext context) : base(context)
        {
        }
    }
}
