using CalculoDeSalario.Data;
using CalculoDeSalario.Models;
using CalculoDeSalario.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CalculoDeSalario.Repository
{
    public class VagasRepository : IVagasRepository, IDisposable
    {
        private ApplicationDbContext context;

        public VagasRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AdicionarVaga(Vagas vaga)
        {
            context.Add(vaga);
        }

        public void AtualizarVaga(Vagas vaga)
        {
            context.Entry(vaga).State = EntityState.Modified;
        }

        public IEnumerable<Vagas> BuscarVagas()
        {
            return context.Vagas.ToList();
        }

        public Vagas BuscarVagasPorId(Guid vagaID)
        {
            return context.Vagas.Find(vagaID);
        }

        public void DeletarVaga(Guid vagaID)
        {
            Vagas vaga = BuscarVagasPorId(vagaID);
            context.Vagas.Remove(vaga);
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
