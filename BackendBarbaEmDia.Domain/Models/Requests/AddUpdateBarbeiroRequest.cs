namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class AddUpdateBarbeiroRequest
    {
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
