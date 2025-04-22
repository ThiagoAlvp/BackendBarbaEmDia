namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdBarbeiro { get; set; }
        public int IdServico { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public TimeSpan Duracao { get; set; }
        public string Status { get; set; } = "Agendado";

        public Cliente? Cliente { get; set; }
        public Barbeiro? Barbeiro { get; set; }
        public Servico? Servico { get; set; }
    }
}