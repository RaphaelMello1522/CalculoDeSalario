using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Cargo
{
    public Guid Id { get; set; }

    public string NomeCargo { get; set; } = null!;

    public string? DescricaoCargo { get; set; }

    public string? ValueHour { get; set; }

    public virtual ICollection<People> People { get; } = new List<People>();
}
