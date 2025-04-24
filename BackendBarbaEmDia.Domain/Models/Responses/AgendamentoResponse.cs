using BackendBarbaEmDia.Domain.Models.Database;
using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class AgendamentoResponse(Agendamento agendamento)
    {
        public int Id { get; set; } = agendamento.Id;
        public int IdCliente { get; set; } = agendamento.IdCliente;
        public int IdBarbeiro { get; set; } = agendamento.IdBarbeiro;
        public int IdServico { get; set; } = agendamento.IdServico;

        public DateTime DataHoraInicio { get; set; } = agendamento.DataHoraInicio;
        public TimeSpan Duracao { get; set; } = agendamento.Duracao;
        public string Status { get; set; } = agendamento.Status;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Cliente? Cliente { get; set; } = agendamento.Cliente;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Barbeiro? Barbeiro { get; set; } = agendamento.Barbeiro;
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Servico? Servico { get; set; } = agendamento.Servico;
    }
}
