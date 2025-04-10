using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRepositorioService : IDisposable
    {
        Task<IEnumerable<List<Repositorio>>> ListarRepositoriosDoUsuario(int id);
        Task<IEnumerable<Repositorio>> ListarRepositoriosPorNome(string nome);
        void AdicionarFavorito(Repositorio favorito);
        void RemoverFavorito(int id);
        Task<IEnumerable<Repositorio>> ListarFavoritos();
    }
}
