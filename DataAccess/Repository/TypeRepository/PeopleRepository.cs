using DataAccess.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.TypeRepository
{
    class PeopleRepository : GenericRepository<People>, IPeopleRepository
    {
        public PeopleRepository(ApplicationDbContext context) : base(context) { }

        public async Task<People> GetPeopleDetails(Guid? id)
        {
            var peopleDetails = await context.People.Where(d => d.Id.Equals(id)).Include("Cargo").FirstAsync();
            return peopleDetails;
        }
    }
}
