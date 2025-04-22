using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;

namespace BackendBarbaEmDia.Infraestructure.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente, BarbeariaContext>, IClienteRepository
    {
        public ClienteRepository(BarbeariaContext context) : base(context)
        {
        }
    }
}
