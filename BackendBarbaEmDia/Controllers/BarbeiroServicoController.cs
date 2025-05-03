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
    [Authorize(AuthenticationSchemes = "Administrador")]
    [ApiController]
    public class BarbeiroServicoController : ControllerBase
    {
        private readonly IBarbeiroServicoService _barbeiroServicoService;

        public BarbeiroServicoController(IBarbeiroServicoService barbeiroServicoService)
        {
            _barbeiroServicoService = barbeiroServicoService;
        }

        // POST api/<BarbeiroServicoController>
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Post([FromBody] AddUpdateBarbeiroServicoRequest request)
        {
            ServiceResult result = await _barbeiroServicoService.CreateBarbeiroServicoAsync(request);

            return this.TrataServiceResult(result);
        }

        // PUT api/<BarbeiroServicoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Put(int id, [FromBody] AddUpdateBarbeiroServicoRequest request)
        {
            ServiceResult result = await _barbeiroServicoService.UpdateBarbeiroServicoAsync(request, id);

            return this.TrataServiceResult(result);
        }

        // DELETE api/<BarbeiroServicoController>/5
        [HttpDelete()]
        public async Task<ActionResult<APIResponse>> Delete(int idBarbeiro, int idServico)
        {
            ServiceResult result = await _barbeiroServicoService.DeleteBarbeiroServicoAsync(idBarbeiro, idServico);

            return this.TrataServiceResult(result);
        }
    }
}
