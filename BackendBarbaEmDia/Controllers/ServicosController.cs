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
    public class ServicosController : ControllerBase
    {
        private readonly IServicoService _servicoService;

        public ServicosController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        // GET: api/<ServicosController>
        [HttpGet]
        public async Task<ActionResult<APIResponse<List<ServicoResponse>>>> Get()
        {
            ServiceResult<List<ServicoResponse>> result = await _servicoService.GetAllAsync();

            return this.TrataServiceResult(result);
        }

        [HttpGet("Ativos")]
        [Authorize(AuthenticationSchemes = "Administrador, Cliente")]
        public async Task<ActionResult<APIResponse<List<ServicoResponse>>>> GetAtivos()
        {
            ServiceResult<List<ServicoResponse>> result = await _servicoService.GetAllAtivosAsync();
            return this.TrataServiceResult(result);
        }

        // GET api/<ServicosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<ServicoResponse>>> Get(int id)
        {
            ServiceResult<ServicoResponse> result = await _servicoService.GetByIdAsync(id);
            return this.TrataServiceResult(result);
        }

        // POST api/<ServicosController>
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Post([FromBody] AddUpdateServicoRequest request)
        {
            ServiceResult result = await _servicoService.CreateAsync(request);
            return this.TrataServiceResult(result);
        }

        // PUT api/<ServicosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Put(int id, [FromBody] AddUpdateServicoRequest request)
        {
            ServiceResult result = await _servicoService.UpdateAsync(request, id);

            return this.TrataServiceResult(result);
        }

        // PUT api/<ServicosController>/Ativar/5
        [HttpPut("Ativar/{id}")]
        public async Task<ActionResult<APIResponse>> Ativar(int id)
        {
            ServiceResult result = await _servicoService.AtivarAsync(id);
            return this.TrataServiceResult(result);
        }

        // PUT api/<ServicosController>/Inativar/5
        [HttpPut("Inativar/{id}")]
        public async Task<ActionResult<APIResponse>> Inativar(int id)
        {
            ServiceResult result = await _servicoService.InativarAsync(id);
            return this.TrataServiceResult(result);
        }

        // DELETE api/<ServicosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            ServiceResult result = await _servicoService.DeleteAsync(id);
            return this.TrataServiceResult(result);
        }
    }
}
