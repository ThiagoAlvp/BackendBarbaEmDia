namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class TopBarbeirosResponse
    {
        public int IdBarbeiro { get; set; }
        public string NomeBarbeiro { get; set; }
        public int TotalAgendamentos { get; set; }

        public TopBarbeirosResponse(int idBarbeiro, string nomeBarbeiro, int totalAgendamentos)
        {
            IdBarbeiro = idBarbeiro;
            NomeBarbeiro = nomeBarbeiro;
            TotalAgendamentos = totalAgendamentos;
        }
    }
}
