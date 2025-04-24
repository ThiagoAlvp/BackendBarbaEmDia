using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Infraestructure.Data.Contexts;
using BackendBarbaEmDia.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public class BarbeiroRepository : Repository<Barbeiro, BarbeariaContext>, IBarbeiroRepository
    {
        private readonly BarbeariaContext _context;
        public BarbeiroRepository(BarbeariaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Barbeiro?> GetBarbeiroEServicosById(int id)
        {
            return await _context.Barbeiros
                .Include(x => x.BarbeiroServicos)
                .ThenInclude(x => x.Servico)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Barbeiro>> GetBarbeirosAtivos(int? idServico = null)
        {
            IQueryable<Barbeiro> query = _context.Barbeiros
                .Where(x => x.Ativo);

            if (idServico is not null)
                query = query
                    .Include(x => x.BarbeiroServicos)
                    .Where(x => x.BarbeiroServicos.Any(y => y.IdServico == idServico));

            return await query.ToListAsync();
        }
    }
}
