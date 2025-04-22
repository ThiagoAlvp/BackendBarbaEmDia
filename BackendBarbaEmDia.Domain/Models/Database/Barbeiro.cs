using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class Barbeiro
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        public bool Ativo { get; set; } = true;

        public List<Agendamento> Agendamentos { get; set; } = [];
        public List<BarbeiroServico> BarbeiroServicos { get; set; } = [];
        public List<Travamento> Travamentos { get; set; } = [];
    }

}
