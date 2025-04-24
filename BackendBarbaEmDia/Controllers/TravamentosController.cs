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
    [ApiController]
    [Authorize(AuthenticationSchemes = "Administrador")]
    public class TravamentosController : ControllerBase
    {
        private readonly ITravamentoService _travamentoService;

        public TravamentosController(ITravamentoService travamentoService)
        {
            _travamentoService = travamentoService;
        }

        // GET: api/<TravamentosController>
        [HttpGet]
        public async Task<ActionResult<APIResponse<List<TravamentoResponse>>>> Get(int? idBarbeiro = null, DateTime? dataFiltro = null)
        {
            if (idBarbeiro is null && dataFiltro is null)
                return this.TrataServiceResult(await _travamentoService.ListarTravamentos());
            else if (idBarbeiro is null)
                return this.TrataServiceResult(await _travamentoService.ListarTravamentosPorData(dataFiltro!.Value));
            else if (dataFiltro is null)
                return this.TrataServiceResult(await _travamentoService.ListarTravamentosPorBarbeiroId(idBarbeiro.Value));
            else
                return this.TrataServiceResult(await _travamentoService.ListarTravamentosPorDataEBarbeiro(dataFiltro.Value, idBarbeiro.Value));
        }

        // GET api/<TravamentosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<TravamentoResponse>>> Get(int id)
        {
            return this.TrataServiceResult(await _travamentoService.ObterTravamentoPorId(id));
        }

        // POST api/<TravamentosController>
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Post([FromBody] AddUpdateTravamentoRequest value)
        {
            return this.TrataServiceResult(await _travamentoService.Criar(value));
        }

        // PUT api/<TravamentosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Put(int id, [FromBody] AddUpdateTravamentoRequest value)
        {
            return this.TrataServiceResult(await _travamentoService.Atualizar(id, value));
        }

        // DELETE api/<TravamentosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            return this.TrataServiceResult(await _travamentoService.Deletar(id));
        }
    }
}
