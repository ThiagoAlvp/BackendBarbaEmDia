namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class AddUpdateAdministradorRequest
    {
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Username { get; set; }
        public string? Senha { get; set; }
    }
}
