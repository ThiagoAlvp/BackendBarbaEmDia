using BackendBarbaEmDia.Domain.Models.Database;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public interface IBarbeiroServicoRepository : IRepository<BarbeiroServico>
    {
        Task<List<BarbeiroServico>> GetBarbeirosByServicoId(int servicoId);
        Task<List<BarbeiroServico>> GetServicosByBarbeiroId(int barbeiroId);
    }
}
