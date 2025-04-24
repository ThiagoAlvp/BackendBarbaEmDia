using BackendBarbaEmDia.Domain.Models.Database;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public interface IBarbeiroRepository : IRepository<Barbeiro>
    {
        Task<Barbeiro?> GetBarbeiroEServicosById(int id);
        Task<List<Barbeiro>> GetBarbeirosAtivos(int? idServico = null);
    }
}
