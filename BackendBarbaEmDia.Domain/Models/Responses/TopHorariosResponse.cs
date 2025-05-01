namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class TopHorariosResponse
    {
        public TimeSpan Horario { get; set; }
        public int TotalAgendamentos { get; set; }
        public TopHorariosResponse(TimeSpan horario, int quantidadeAgendamentos)
        {
            Horario = horario;
            TotalAgendamentos = quantidadeAgendamentos;
        }
    }
}
