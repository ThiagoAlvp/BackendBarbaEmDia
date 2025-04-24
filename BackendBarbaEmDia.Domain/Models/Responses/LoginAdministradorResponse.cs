namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class LoginAdministradorResponse
    {
        public required AdministradorResponse Administrador { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
