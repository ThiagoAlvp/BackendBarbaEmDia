using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;
using BackendBarbaEmDia.Infraestructure.Data.Repositories;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public class BarbeiroServicoRepository : Repository<BarbeiroServico, BarbeariaContext>, IBarbeiroServicoRepository
    {
        private readonly BarbeariaContext _context;

        public BarbeiroServicoRepository(BarbeariaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BarbeiroServico>> GetServicosByBarbeiroId(int barbeiroId)
        {
            return await GetListWithIncludesAsync(x => x.IdBarbeiro == barbeiroId, x => x.Servico);
        }

        public async Task<List<BarbeiroServico>> GetBarbeirosByServicoId(int servicoId)
        {
            return await GetListWithIncludesAsync(x => x.IdServico == servicoId, x => x.Barbeiro);
        }
    }
}
