using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGitHubClient : IDisposable
    {
        Task<IEnumerable<Repositorio>> BuscarRepositoriosAsync(string name);
    }
}
