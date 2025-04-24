using BackendBarbaEmDia.Domain.Models.Database;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class ServicoResponse(Servico servico)
    {
        public int Id { get; set; } = servico.Id;
        public string Descricao { get; set; } = servico.Descricao;
        public bool Ativo { get; set; } = servico.Ativo;
        public TimeSpan DuracaoPadrao { get; set; } = servico.DuracaoPadrao;
        public decimal Preco { get; set; } = servico.Preco;
    }
}
