using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Interfaces.Services
{
    public interface IEstatisticasService
    {
        Task<ServiceResult<List<TopHorariosResponse>>> GetTopHorariosAgendamentos();
        Task<ServiceResult<List<TopDiasSemanaResponse>>> GetTopDiasSemanaAgendamentos();
        Task<ServiceResult<List<TopBarbeirosResponse>>> GetTopBarbeirosAgendamentos();
        Task<ServiceResult<List<TopServicosResponse>>> GetTopServicosAgendamentos();
        Task<ServiceResult<ResumoMesResponse>> GetResumoMes();
    }
}
