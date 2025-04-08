using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendBarbaEmDia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        // POST api/<ClientesController>/Login
        [HttpPost("Login")]
        public ActionResult<APIResponse<LoginClienteResponse>> Login([FromBody] LoginClienteRequest request)
        {
            if (request.NrTelefone.Length < 11)
                return BadRequest(new APIResponse<LoginClienteResponse>(false, "Número de telefone deve ter pelo menos 11 digítos."));

            LoginClienteResponse loginClienteResponse =
                new()
                {
                    Id = 1,
                    Nome = "Cliente Teste",
                    Telefone = "11999999999",
                    Token = "token",
                };

            return Ok(new APIResponse<LoginClienteResponse>(loginClienteResponse,"Login realizado com sucesso!"));
        }
    }
}
