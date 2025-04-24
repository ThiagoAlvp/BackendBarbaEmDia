using BackendBarbaEmDia.Domain.Models.Database;

namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class AddUpdateBarbeiroServicoRequest
    {
        public int IdBarbeiro { get; set; }
        public int IdServico { get; set; }
        public TimeSpan? TempoPersonalizado { get; set; }
    }
}
