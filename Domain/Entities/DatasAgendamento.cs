using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DatasAgendamento
{
    public Guid Id { get; set; }

    public DateTime DataDisponivel { get; set; }

    public virtual ICollection<Agendamento> Agendamentos { get; } = new List<Agendamento>();
}
