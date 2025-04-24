using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using BackendBarbaEmDia.Extensions;
using BackendBarbaEmDia.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Administrador")]
    public class BarbeirosController : ControllerBase
    {
        private readonly IBarbeiroService _barbeiroService;

        public BarbeirosController(IBarbeiroService barbeiroService)
        {
            _barbeiroService = barbeiroService;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse<List<BarbeiroResponse>>>> Get()
        {
            ServiceResult<List<BarbeiroResponse>> result = await _barbeiroService.GetAllAsync();
            return this.TrataServiceResult(result);
        }

        [HttpGet("Ativos")]
        [Authorize(AuthenticationSchemes = "Administrador, Cliente")]
        public async Task<ActionResult<APIResponse<List<BarbeiroResponse>>>> GetAtivos([FromQuery] int? idServico = null)
        {
            ServiceResult<List<BarbeiroResponse>> result = await _barbeiroService.GetAllAtivosAsync(idServico);
            return this.TrataServiceResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<BarbeiroResponse>>> Get(int id)
        {
            ServiceResult<BarbeiroResponse> result = await _barbeiroService.GetByIdAsync(id);

            return this.TrataServiceResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> Post([FromBody] AddUpdateBarbeiroRequest request)
        {
            ServiceResult result = await _barbeiroService.CreateAsync(request);

            return this.TrataServiceResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Put(int id, [FromBody] AddUpdateBarbeiroRequest request)
        {
            ServiceResult result = await _barbeiroService.UpdateAsync(request, id);

            return this.TrataServiceResult(result);
        }

        [HttpPut("Ativar/{id}")]
        public async Task<ActionResult<APIResponse>> Ativar(int id)
        {
            ServiceResult result = await _barbeiroService.AtivarAsync(id);
            return this.TrataServiceResult(result);
        }

        [HttpPut("Inativar/{id}")]
        public async Task<ActionResult<APIResponse>> Inativar(int id)
        {
            ServiceResult result = await _barbeiroService.InativarAsync(id);
            return this.TrataServiceResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            ServiceResult result = await _barbeiroService.DeleteAsync(id);
            return this.TrataServiceResult(result);
        }
    }
}
