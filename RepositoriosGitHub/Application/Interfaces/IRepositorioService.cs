using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRepositorioService : IDisposable
    {
        /// <summary>
        /// Lista os repositórios de um determinado usuário.
        /// </summary>
        /// <param name="nome">O nome do usuário em questão.</param>
        Task<IEnumerable<Repositorio>> ListarDoUsuario(string nome);
        /// <summary>
        /// Lista os repositórios filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome que o repositório deve conter</param>
        Task<IEnumerable<Repositorio>> ListarPorNome(string nome);
        /// <summary>
        /// Lista os repositórios ordenando por relevância.
        /// </summary>
        /// <param name="asc">A ordenação deve ser crescente ou decrescente</param>
        Task<IEnumerable<Repositorio>> ListarPorRelevanciaAsync(bool asc);
    }
}
