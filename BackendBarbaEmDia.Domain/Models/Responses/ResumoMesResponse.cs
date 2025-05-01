namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class ResumoMesResponse
    {
        public string Mes { get; set; }
        public int TotalAgendamentos { get; set; }
        public int TotalAgendamentosCancelados { get; set; }
        public decimal TotalFaturamento { get; set; }

        public ResumoMesResponse(string mes, int totalAgendamentos, int totalAgendamentosCancelados, decimal totalFaturamento)
        {
            Mes = mes;
            TotalAgendamentos = totalAgendamentos;
            TotalAgendamentosCancelados = totalAgendamentosCancelados;
            TotalFaturamento = totalFaturamento;
        }
    }
}
