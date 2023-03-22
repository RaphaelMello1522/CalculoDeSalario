using CalculoDeSalario.Models;

namespace CalculoDeSalario.Repository.IRepository
{
    public interface IVagasRepository : IDisposable
    {
        IEnumerable<Vagas> BuscarVagas();
        Vagas BuscarVagasPorId(Guid vagaID);
        void AdicionarVaga(Vagas vaga);
        void DeletarVaga(Guid vagaID);
        void AtualizarVaga(Vagas vaga);
        void Salvar();
    }
}
