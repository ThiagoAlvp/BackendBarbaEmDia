namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class LoginClienteResponse(ClienteResponse cliente)
    {
        public ClienteResponse Cliente { get; set; } = cliente;
        public string Token { get; set; } = "";
    }
}
