namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class AddUpdateAgendamento
    {
        public int IdCliente { get; set; }
        public int IdBarbeiro { get; set; }
        public int IdServico { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public TimeSpan Duracao { get; set; }
    }
}
