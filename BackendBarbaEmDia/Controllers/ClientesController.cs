using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using BackendBarbaEmDia.Extensions;
using BackendBarbaEmDia.Models.Responses;
using BackendBarbaEmDia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ITokenService _tokenService;

        public ClientesController(IClienteService clienteService,
                                  ITokenService tokenService)
        {
            _clienteService = clienteService;
            _tokenService = tokenService;
        }

        // POST api/<ClientesController>/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse<LoginClienteResponse>>> Login([FromBody] LoginClienteRequest request)
        {
            ServiceResult<LoginClienteResponse> result = await _clienteService.VerificaClienteLogin(request);

            if (!result.Success)
                return this.TrataServiceResult(result);

            _tokenService.PreencherToken(result.Data!);

            return this.TrataServiceResult(result);
        }
    }
}
