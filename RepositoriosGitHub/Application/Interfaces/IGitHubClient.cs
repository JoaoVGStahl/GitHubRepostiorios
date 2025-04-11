using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGitHubClient : IDisposable
    {
        Task<IEnumerable<Repositorio>> BuscarAsync(string name);
        Task<IEnumerable<Repositorio>> BuscarAsync();
        Task<IEnumerable<Repositorio>> BuscarDoUsuario(string name);
    }
}
