using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Agendamento
{
    public Guid Id { get; set; }

    public DateTime DataAgendamento { get; set; }

    public Guid DatasAgendamentoId { get; set; }

    public virtual DatasAgendamento DatasAgendamento { get; set; } = null!;
}
