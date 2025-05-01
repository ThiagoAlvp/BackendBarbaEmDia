using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Responses;
using BackendBarbaEmDia.Extensions;
using BackendBarbaEmDia.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Administrador")]
    [ApiController]
    public class EstatisticasController : ControllerBase
    {
        private readonly IEstatisticasService _estatisticasService;

        public EstatisticasController(IEstatisticasService estatisticasService)
        {
            _estatisticasService = estatisticasService;
        }

        [HttpGet("TopHorarios")]
        public async Task<ActionResult<APIResponse<List<TopHorariosResponse>>>> GetTopHorariosAgendamentos()
        {
            return this.TrataServiceResult(await _estatisticasService.GetTopHorariosAgendamentos());
        }

        [HttpGet("TopDiasSemana")]
        public async Task<ActionResult<APIResponse<List<TopDiasSemanaResponse>>>> GetTopDiasSemanaAgendamentos()
        {
            return this.TrataServiceResult(await _estatisticasService.GetTopDiasSemanaAgendamentos());
        }

        [HttpGet("TopBarbeiros")]
        public async Task<ActionResult<APIResponse<List<TopBarbeirosResponse>>>> GetTopBarbeirosAgendamentos()
        {
            return this.TrataServiceResult(await _estatisticasService.GetTopBarbeirosAgendamentos());
        }

        [HttpGet("TopServicos")]
        public async Task<ActionResult<APIResponse<List<TopServicosResponse>>>> GetTopServicosAgendamentos()
        {
            return this.TrataServiceResult(await _estatisticasService.GetTopServicosAgendamentos());
        }

        [HttpGet("Resumo")]
        public async Task<ActionResult<APIResponse<ResumoMesResponse>>> GetResumoMes()
        {
            return this.TrataServiceResult(await _estatisticasService.GetResumoMes());
        }
    }
}
