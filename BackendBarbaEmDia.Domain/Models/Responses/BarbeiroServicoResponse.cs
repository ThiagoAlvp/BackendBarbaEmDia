using BackendBarbaEmDia.Domain.Models.Database;
using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class BarbeiroServicoResponse(BarbeiroServico barbeiroServico)
    {
        public ServicoResponse? Servico { get; set; } = barbeiroServico.Servico is not null
            ? new ServicoResponse(barbeiroServico.Servico)
            : null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TimeSpan? TempoPersonalizado { get; set; } = barbeiroServico.TempoPersonalizado ?? null;
    }
}
