using BackendBarbaEmDia.Domain.Models.Database;
using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class TravamentoResponse(Travamento travamento)
    {
        public int Id { get; set; } = travamento.Id;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BarbeiroResponse? Barbeiro { get; set; } = travamento.Barbeiro is null ? null : new(travamento.Barbeiro);
        public DateTime DataHoraInicio { get; set; } = travamento.DataHoraInicio;
        public DateTime DataHoraFim { get; set; } = travamento.DataHoraFim;
        public string Motivo { get; set; } = travamento.Motivo;
    }
}
