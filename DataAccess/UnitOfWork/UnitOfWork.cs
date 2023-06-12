using DataAccess.Data;
using DataAccess.Repository.TypeRepository;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            People = new PeopleRepository(this.context);
            Cargo = new CargoRepository(this.context);
        }

        public IPeopleRepository People { get; private set; }
        public ICargoRepository Cargo { get; private set; }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
