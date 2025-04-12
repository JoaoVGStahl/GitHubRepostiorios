using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
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

        public async Task<IEnumerable<RepositorioDTO>> ListarDoUsuarioAsync(string nome)
        {
            var repositorio = await _cliente.BuscarDoUsuario(nome);

            if (!repositorio.Any()) return Enumerable.Empty<RepositorioDTO>();

            return repositorio.Select(RepositorioMapper.ToDTO);
        }

        public async Task<IEnumerable<RepositorioDTO>> ListarPorNomeAsync(string nome)
        {
            var repositorios = await _cliente.BuscarAsync(nome);

            if (!repositorios.Any()) return Enumerable.Empty<RepositorioDTO>();

            return repositorios.Select(RepositorioMapper.ToDTO);
        }

        public async Task<IEnumerable<RepositorioRevelanteDTO>> ListarPorRelevanciaAsync(bool asc)
        {
            var repositorios = await _cliente.BuscarAsync();

            if (!repositorios.Any()) return Enumerable.Empty<RepositorioRevelanteDTO>();

            var repositoriosRelevantes = repositorios
                .Select(repo => RepositorioMapper.ToRelevanteDTO(repo, CalcularRelevancia(repo)));

            return OrdenarPorRelevancia(repositoriosRelevantes, asc);
        }

        private static IEnumerable<RepositorioRevelanteDTO> OrdenarPorRelevancia(IEnumerable<RepositorioRevelanteDTO> repositorios, bool asc)
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

            var estrelas = Math.Min(item.StargazersCount, RelevanciaConfig.MAX_STARS);
            var forks = Math.Min(item.ForksCount, RelevanciaConfig.MAX_FORKS);
            var watchers = Math.Min(item.WatchersCount, RelevanciaConfig.MAX_WATCHERS);

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
