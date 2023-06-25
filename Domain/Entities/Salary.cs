using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Salary
{
    public Guid Id { get; set; }

    public Guid PeopleId { get; set; }

    public Guid CargoId { get; set; }

    public DateTime TimeWorkStart { get; set; }

    public DateTime TimeWorkEnd { get; set; }

    public TimeSpan TotalTimeWorked { get; set; }

    public double Total { get; set; }

    public virtual People People { get; set; } = null!;
}
