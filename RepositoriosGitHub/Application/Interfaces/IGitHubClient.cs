using Domain.Entities;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface que define operações para interação com o GitHub, como buscar repositórios
    /// e buscar repositórios de um usuário específico.
    /// </summary>
    public interface IGitHubClient : IDisposable
    {
        /// <summary>
        /// Busca repositórios com base no nome fornecido.
        /// </summary>
        /// <param name="name">Nome do repositório a ser buscado.</param>
        /// <returns>Uma lista de repositórios encontrados.</returns>
        Task<IEnumerable<Repositorio>> BuscarAsync(string name);

        /// <summary>
        /// Busca todos os repositórios disponíveis.
        /// </summary>
        /// <returns>Uma lista de todos os repositórios.</returns>
        Task<IEnumerable<Repositorio>> BuscarAsync();

        /// <summary>
        /// Busca os repositórios de um usuário específico no GitHub.
        /// </summary>
        /// <param name="name">Nome do usuário cujos repositórios serão buscados.</param>
        /// <returns>Uma lista de repositórios do usuário.</returns>
        Task<IEnumerable<Repositorio>> BuscarDoUsuario(string name);
    }
}
