namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class AddUpdateTravamentoRequest
    {
        public int IdBarbeiro { get; set; }
        public string Motivo { get; set; } = "";
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
