using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class AddUpdateAgendamentoRequest
    {
        public int IdCliente { get; set; }
        public int IdBarbeiro { get; set; }
        public int IdServico { get; set; }
        [JsonIgnore]
        public TimeSpan? Duracao { get; set; }
        public DateTime DataHoraInicio { get; set; }       
    }
}
