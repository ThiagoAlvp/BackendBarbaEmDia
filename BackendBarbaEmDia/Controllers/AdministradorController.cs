using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using BackendBarbaEmDia.Domain.Services;
using BackendBarbaEmDia.Extensions;
using BackendBarbaEmDia.Models.Responses;
using BackendBarbaEmDia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Administrador")]
    public class AdministradorController : ControllerBase
    {
        public readonly IAdministradorService _administradorService;
        public readonly ITokenService _tokenService;

        public AdministradorController(IAdministradorService administradorService,
                                       ITokenService tokenService)
        {
            _administradorService = administradorService;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse<LoginAdministradorResponse>>> Login(LoginAdministradorRequest request)
        {
            ServiceResult<LoginAdministradorResponse> result = await _administradorService.VerificaAdministradorLogin(request);

            if (!result.Success)
                return this.TrataServiceResult(result);

            _tokenService.PreencherTokenAdministrador(result.Data!);

            return this.TrataServiceResult(result);
        }
    }
}
