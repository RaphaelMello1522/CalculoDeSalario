using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPeopleRepository : IGenericRepository<People>
    {
        Task<People> GetPeopleDetails(Guid? id);
        IEnumerable<People> GetPeopleWtithCargo();

    }
}
