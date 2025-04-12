using Domain.Entities;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface para gerenciar os favoritos na memória.
    /// </summary>
    public interface IFavoritosStorage : IDisposable
    {
        /// <summary>
        /// Adiciona um repositório na memoria do serviço.
        /// </summary>
        /// <param name="favorito">O repositório a ser adicionado.</param>
        void Adicionar(Repositorio favorito);
        /// <summary>
        /// Remove um favorito da memória.
        /// </summary>
        /// <param name="id">Id do repositório a ser removido.</param>
        void Remover(int id);
        /// <summary>
        /// Obter um favorito pelo Id.
        /// </summary>
        /// <param name="id">Id do repositório.</param>
        /// <returns>Um Repositorio ou null</returns>
        Repositorio ObterPorId(int id);
        /// <summary>
        /// Todos os favoritos.
        /// </summary>
        /// <returns>Uma lista de todos os repositórios favoritos.</returns>
        IEnumerable<Repositorio> ListarFavoritos();
    }
}
