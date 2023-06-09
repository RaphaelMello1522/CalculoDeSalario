using DataAccess.Data;
using Domain.Entities;
using Domain.Interfaces;
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
    }
}
