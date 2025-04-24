namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class Servico
    {
        public int Id { get; set; }
        public required string Descricao { get; set; }
        public TimeSpan DuracaoPadrao { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; } = true;

        public List<Agendamento> Agendamentos { get; set; } = [];
        public List<BarbeiroServico> BarbeiroServicos { get; set; } = [];
    }

}
