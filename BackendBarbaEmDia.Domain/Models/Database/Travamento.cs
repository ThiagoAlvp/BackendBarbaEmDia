namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class Travamento
    {
        public int Id { get; set; }
        public int IdBarbeiro { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public Barbeiro? Barbeiro { get; set; }
    }
}
