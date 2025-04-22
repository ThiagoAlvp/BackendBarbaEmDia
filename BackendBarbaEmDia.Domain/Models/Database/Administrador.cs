namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class Administrador
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public required string Username { get; set; }
        public required string Senha { get; set; }
    }

}
