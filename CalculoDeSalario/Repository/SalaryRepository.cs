using CalculoDeSalario.Data;
using CalculoDeSalario.Models;
using CalculoDeSalario.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CalculoDeSalario.Repository
{
    public class SalaryRepository : ISalaryRepository, IDisposable
    {
        private ApplicationDbContext context;

        public SalaryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AdicionarSalario(Salary salary)
        {
            context.Salary.Add(salary);
        }

        public void AtualizarSalario(Salary salary)
        {
            context.Entry(salary).State = EntityState.Modified;
        }

        public Salary BuscarSalarioPorId(Guid salaryID)
        {
            return context.Salary.Find(salaryID);
        }

        public IEnumerable<Salary> BuscarSalarios()
        {
            var salaryList = new List<Salary>();
            var salary = new Salary();
            var salaryContext = context.Salary.Include("People").Include("People.Cargo");

            foreach (var item in salaryContext)
            {
                salary.Id = item.Id;
                salary.TimeWorkStart = item.TimeWorkStart;
                salary.TimeWorkEnd = item.TimeWorkEnd;
                salary.TotalTimeWorked = item.TimeWorkEnd - item.TimeWorkStart;
                salary.Total = salary.TotalTimeWorked.TotalHours * Convert.ToDouble(item.People.Cargo.ValueHour);

                salaryList.Add(item);
            }

            return salaryList;
        }

        public void DeletarSalario(Guid salaryID)
        {
            Salary salary = BuscarSalarioPorId(salaryID);
            context.Salary.Remove(salary);
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
