namespace BackendBarbaEmDia.Domain.Models.Database
{
    public class ParametrizacaoHorarioFuncionamento
    {
        public int Id { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
    }
}
