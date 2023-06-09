namespace Domain.Entities
{
    public class Agendamento
    {
        public Guid Id { get; set; }
        public DateTime DataAgendamento { get; set;}

        public virtual DatasAgendamento DatasAgendamento { get; set;}
    }
}
