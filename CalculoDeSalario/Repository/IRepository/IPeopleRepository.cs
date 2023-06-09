
using Domain.Entities;

namespace CalculoDeSalario.Repository.IRepository
{
    public interface IPeopleRepository : IDisposable
    {
        IEnumerable<People> BuscarPessoas();
        People BuscarPessoaPorId(Guid id);
        void AdicionarPessoa(People id);
        void DeletarPessoa(Guid id);
        void AtualizarPessoa(People id);
        void Salvar();
    }
}
