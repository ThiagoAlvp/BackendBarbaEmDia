using BackendBarbaEmDia.Domain.Models.Database;
using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class BarbeiroResponse(Barbeiro barbeiro)
    {
        public int Id { get; set; } = barbeiro.Id;
        public string Nome { get; set; } = barbeiro.Nome;
        public bool Ativo { get; set; } = barbeiro.Ativo;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<BarbeiroServicoResponse> barbeiroServicos { get; set; } = barbeiro.BarbeiroServicos
            .Select(x => new BarbeiroServicoResponse(x))
            .ToList();
    }
}
