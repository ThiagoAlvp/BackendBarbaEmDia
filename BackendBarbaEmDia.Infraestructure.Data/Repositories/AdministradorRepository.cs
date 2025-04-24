using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;
using BackendBarbaEmDia.Infraestructure.Data.Repositories;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public class AdministradorRepository : Repository<Administrador, BarbeariaContext>, IAdministradorRepository
    {
        public AdministradorRepository(BarbeariaContext context) : base(context)
        {
        }
    }
}
