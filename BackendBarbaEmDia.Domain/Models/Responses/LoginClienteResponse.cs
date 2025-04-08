namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class LoginClienteResponse
    {
        public required int Id { get; set; }
        public required string Token { get; set; }
        public required string Nome { get; set; }
        public string Telefone { get; set; } = string.Empty;
    }
}
