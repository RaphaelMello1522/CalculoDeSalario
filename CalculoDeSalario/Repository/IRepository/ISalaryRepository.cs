using CalculoDeSalario.Models;

namespace CalculoDeSalario.Repository.IRepository
{
    public interface ISalaryRepository : IDisposable
    {
        IEnumerable<Salary> BuscarSalarios();
        Salary BuscarSalarioPorId(Guid salaryID);
        void AdicionarSalario(Salary salary);
        void DeletarSalario(Guid salaryID);
        void AtualizarSalario(Salary salary);
        void Salvar();
    }
}
