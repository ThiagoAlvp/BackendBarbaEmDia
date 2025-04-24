namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class AddUpdateServicoRequest
    {
        public string Descricao { get; set; } = string.Empty;
        public TimeSpan DuracaoPadrao { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
