using Application.Interfaces;
using Domain.Constantes;
using Domain.Entities;

namespace Application.Services
{
    public class RepositorioService : IRepositorioService
    {
        private readonly IGitHubClient _cliente;
        public RepositorioService(IGitHubClient gitHubClient)
        {
            _cliente = gitHubClient;
        }

        public async Task<IEnumerable<Repositorio>> ListarDoUsuario(string nome)
        {
            return await _cliente.BuscarDoUsuario(nome);
        }

        public async Task<IEnumerable<Repositorio>> ListarPorNome(string nome)
        {
            return await _cliente.BuscarAsync(nome);
        }

        public async Task<IEnumerable<Repositorio>> ListarPorRelevanciaAsync(bool asc)
        {
            var repositorios = await _cliente.BuscarAsync();

            foreach (var repositorio in repositorios)
            {
                repositorio.Relevancia = CalcularRelevancia(repositorio);
            }

            return OrdenarPorRelevancia(repositorios, asc);
        }

        private static IEnumerable<Repositorio> OrdenarPorRelevancia(IEnumerable<Repositorio> repositorios, bool asc)
        {
            return asc
                ? repositorios.OrderBy(r => r.Relevancia)
                : repositorios.OrderByDescending(r => r.Relevancia);
        }

        private static double CalcularRelevancia(Repositorio item)
        {
            /*
                A relevância é calculada com base nos seguintes critérios:
                - Forks (maior peso): indicam o quanto um repositório é usado.
                - Estrelas (peso médio): indica popularidade.
                - Watchers (menor peso): indica interesse.

                Limites máximos com Math.Min evita distorções.
                Exemplo: um repositório extremamente curtido mas com poucos forks não será mais relevante
                que um repositório com muitos forks.

                RelevanciaConfig centralizando as constantes de relevância, facilitando a manutenção e reutilização.
            */

            var estrelas = Math.Min(item.Stars, RelevanciaConfig.MAX_STARS);
            var forks = Math.Min(item.Forks, RelevanciaConfig.MAX_FORKS);
            var watchers = Math.Min(item.Watchers, RelevanciaConfig.MAX_WATCHERS);

            return (estrelas * RelevanciaConfig.PESO_STARS) +
                   (forks * RelevanciaConfig.PESO_FORKS) +
                   (watchers * RelevanciaConfig.PESO_WATCHERS);
        }

        public void Dispose()
        {
            _cliente.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
