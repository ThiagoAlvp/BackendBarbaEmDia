using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using BackendBarbaEmDia.Extensions;
using BackendBarbaEmDia.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Administrador, Cliente")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        // GET: api/<AgendamentoController>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Administrador")]
        public async Task<ActionResult<APIResponse<List<AgendamentoResponse>>>> Get(int? idBarbeiro = null, int? idCliente = null, DateTime? data = null)
        {
            return this.TrataServiceResult(await _agendamentoService.ListarAgendamentos(idBarbeiro, idCliente, data));
        }

        // GET: api/<AgendamentoController>
        [HttpGet("Cliente/{idCliente}")]
        [Authorize(AuthenticationSchemes = "Administrador")]
        public async Task<ActionResult<APIResponse<List<AgendamentoResponse>>>> GetAgendamentosCliente([FromRoute]int idCliente, int? idBarbeiro = null, DateTime? data = null)
        {
            return this.TrataServiceResult(await _agendamentoService.ListarAgendamentos(idBarbeiro, idCliente, data));
        }

        // GET api/<AgendamentoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<AgendamentoResponse>>> Get(int id)
        {
            return this.TrataServiceResult(await _agendamentoService.ObterAgendamentoPorId(id));
        }

        // POST api/<AgendamentoController>/Agendar
        [HttpPost("Agendar")]
        public async Task<ActionResult<APIResponse>> Agendar([FromBody] AddUpdateAgendamentoRequest request)
        {
            var result = await _agendamentoService.Agendar(request);
            return this.TrataServiceResult(result);
        }

        // PUT api/<AgendamentoController>/Reagendar/5
        [HttpPut("Reagendar/{id}")]
        public async Task<ActionResult<APIResponse>> Reagendar(int id, [FromBody] AddUpdateAgendamentoRequest request)
        {
            var result = await _agendamentoService.Reagendar(id, request);
            return this.TrataServiceResult(result);
        }

        // PUT api/<AgendamentoController>/Cancelar/5
        [HttpPut("Cancelar/{id}")]
        public async Task<ActionResult<APIResponse>> Cancelar(int id)
        {
            var result = await _agendamentoService.Cancelar(id);
            return this.TrataServiceResult(result);
        }

        // GET api/<AgendamentoController>/HorariosIndisponiveis
        [HttpGet("HorariosIndisponiveis")]
        public async Task<ActionResult<APIResponse<List<DateTime>>>> GetHorariosIndisponiveis(int idServico, int? idBarbeiro = null, TimeSpan? horarioPreferencialInicial = null, TimeSpan? horarioPreferencialFinal = null)
        {
            return this.TrataServiceResult(
                await _agendamentoService.ObterHorariosIndisponiveis(
                    idServico,
                    idBarbeiro,
                    horarioPreferencialInicial, 
                    horarioPreferencialFinal
                )
            );
        }
    }
}
