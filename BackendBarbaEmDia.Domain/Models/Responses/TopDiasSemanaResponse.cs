namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class TopDiasSemanaResponse
    {
        public string DiaSemana { get; set; }
        public int TotalAgendamentos { get; set; }
        public TopDiasSemanaResponse(string diaSemana, int quantidadeAgendamentos)
        {
            DiaSemana = diaSemana;
            TotalAgendamentos = quantidadeAgendamentos;
        }
    }
}
