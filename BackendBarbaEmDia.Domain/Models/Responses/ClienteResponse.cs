using BackendBarbaEmDia.Domain.Models.Database;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class ClienteResponse(Cliente cliente)
    {
        public int Id { get; set; } = cliente.Id;
        public string? Nome { get; set; } = cliente.Nome;
        public string? Telefone { get; set; } = cliente.Telefone;
    }
}
