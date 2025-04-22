namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public required string Telefone { get; set; }

        public List<Agendamento> Agendamentos { get; set; } = [];
    }

}
