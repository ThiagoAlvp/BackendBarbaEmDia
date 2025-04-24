using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Extensions;
using BackendBarbaEmDia.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Administrador, Cliente")]
    [ApiController]
    public class ParametrizacaoHorariosController : ControllerBase
    {
        private readonly IParametrizacaoHorarioFuncionamentoService _parametrizacaoHorariosService;

        public ParametrizacaoHorariosController(IParametrizacaoHorarioFuncionamentoService parametrizacaoHorariosService)
        {
            _parametrizacaoHorariosService = parametrizacaoHorariosService;
        }

        // GET: api/<ParametrizacaoHorariosController>
        [HttpGet]
        public async Task<ActionResult<APIResponse<List<ParametrizacaoHorarioFuncionamento>>>> Get()
        {
            return this.TrataServiceResult(await _parametrizacaoHorariosService.GetParametrizacaoHorarios());
        }
    }
}
