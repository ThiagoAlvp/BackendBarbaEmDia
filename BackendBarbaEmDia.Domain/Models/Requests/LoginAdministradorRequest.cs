namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class LoginAdministradorRequest
    {
        public required string Username { get; set; }
        public required string Senha { get; set; }
    }
}
