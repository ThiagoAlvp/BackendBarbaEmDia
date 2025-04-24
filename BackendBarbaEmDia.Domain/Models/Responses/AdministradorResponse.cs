using BackendBarbaEmDia.Domain.Models.Database;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class AdministradorResponse(Administrador administrador)
    {
        public int Id { get; set; } = administrador.Id;
        public string? Nome { get; set; } = administrador.Nome;
        public string? Telefone { get; set; } = administrador.Telefone;
        public string Username { get; set; } = administrador.Username;
    }
}
