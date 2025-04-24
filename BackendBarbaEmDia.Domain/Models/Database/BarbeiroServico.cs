using System.ComponentModel.DataAnnotations;

namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class BarbeiroServico
    {
        [Key]
        public int Id { get; set; }

        public int IdBarbeiro { get; set; }
        public int IdServico { get; set; }
        public TimeSpan? TempoPersonalizado { get; set; }

        public Barbeiro? Barbeiro { get; set; }
        public Servico? Servico { get; set; }
    }

}