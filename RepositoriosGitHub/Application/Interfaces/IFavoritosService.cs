using Application.DTOs;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface para gerenciar os repositórios favoritos.
    /// </summary>
    public interface IFavoritosService : IDisposable 
    {
        /// <summary>
        /// Adiciona um repositório aos favoritos.
        /// </summary>
        /// <param name="favorito">O repositório a ser adicionado.</param>
        void Adicionar(RepositorioDTO favorito);

        /// <summary>
        /// Remove um repositório dos favoritos.
        /// </summary>
        /// <param name="id">O ID do repositório a ser removido.</param>
        void Remover(int id);

        /// <summary>
        /// Obtém um repositório favorito pelo ID.
        /// </summary>
        /// <param name="id">O ID do repositório.</param>
        /// <returns>RepositorioDTO ou null</returns>
        RepositorioDTO? ObterPorId(int id);

        /// <summary>
        /// todos os repositórios favoritos.
        /// </summary>
        /// <returns>Uma lista de todos os favoritos.</returns>
        IEnumerable<RepositorioDTO> ListarFavoritos();
    }
}
