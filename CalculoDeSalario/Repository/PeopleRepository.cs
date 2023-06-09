using CalculoDeSalario.Repository.IRepository;
using DataAccess.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculoDeSalario.Repository
{
    public class PeopleRepository : IPeopleRepository, IDisposable
    {
        private ApplicationDbContext context;

        public PeopleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AdicionarPessoa(People id)
        {
            context.People.Add(id);
        }

        public void AtualizarPessoa(People id)
        {
            context.Entry(id).State = EntityState.Modified;
        }

        public People BuscarPessoaPorId(Guid id)
        {
            return context.People.Find(id);

        }

        public IEnumerable<People> BuscarPessoas()
        {
            var peopleList = new List<People>();
            var people = new People();

            foreach (var item in context.People.Include("Cargo").AsNoTracking())
            {
                people.Id = item.Id;
                people.Name = item.Name;

                peopleList.Add(item);
            }

            return peopleList;
        }

        public void DeletarPessoa(Guid pessoaID)
        {
            People people = BuscarPessoaPorId(pessoaID);
            context.People.Remove(people);
        }
        public void Salvar()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
