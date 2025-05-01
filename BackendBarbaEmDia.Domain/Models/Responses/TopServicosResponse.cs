namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class TopServicosResponse
    {
        public int IdServico { get; set; }
        public string NomeServico { get; set; }
        public int TotalAgendamentos { get; set; }

        public TopServicosResponse(int idServico, string nomeServico, int totalAgendamentos)
        {
            IdServico = idServico;
            NomeServico = nomeServico;
            TotalAgendamentos = totalAgendamentos;
        }        
    }
}
