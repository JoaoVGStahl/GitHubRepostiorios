using Application.DTOs;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface com operações de busca de repositórios.
    /// </summary>
    public interface IRepositorioService : IDisposable
    {
        /// <summary>
        /// Lista os repositórios de um determinado usuário.
        /// </summary>
        /// <param name="nome">O nome do usuário em questão.</param>
        /// <returns>Lista dos repositório do usuário</returns>
        Task<IEnumerable<RepositorioDTO>> ListarDoUsuarioAsync(string nome);
        /// <summary>
        /// Lista os repositórios filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome que o repositório deve conter</param>
        /// <returns>Uma lista de repositório da pesquisa</returns>
        Task<IEnumerable<RepositorioDTO>> ListarPorNomeAsync(string nome);
        /// <summary>
        /// Lista os repositórios ordenando por relevância.
        /// </summary>
        /// <param name="asc">A ordenação deve ser crescente ou decrescente</param>
        /// <returns>Uma lista de repositório da pesquisa, ordenados por relevancia.</returns>
        Task<IEnumerable<RepositorioRevelanteDTO>> ListarPorRelevanciaAsync(bool asc);
    }
}
