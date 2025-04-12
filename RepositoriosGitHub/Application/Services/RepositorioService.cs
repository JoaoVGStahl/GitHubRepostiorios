using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Services
{
    public class RepositorioService : IRepositorioService
    {
        private readonly IGitHubClient _client;
        private readonly IRelevanciaService _relevanciaService;
        public RepositorioService(IGitHubClient gitHubClient, 
                                  IRelevanciaService relevanciaService)
        {
            _client = gitHubClient;
            _relevanciaService = relevanciaService;
        }

        public async Task<IEnumerable<RepositorioDTO>> ListarDoUsuarioAsync(string nome)
        {
            var repositorio = await _client.BuscarDoUsuario(nome);

            if (!repositorio.Any()) return Enumerable.Empty<RepositorioDTO>();

            return repositorio.Select(RepositorioMapper.ToDTO);
        }

        public async Task<IEnumerable<RepositorioDTO>> ListarPorNomeAsync(string nome)
        {
            var repositorios = await _client.BuscarAsync(nome);

            if (!repositorios.Any()) return Enumerable.Empty<RepositorioDTO>();

            return repositorios.Select(RepositorioMapper.ToDTO);
        }

        public async Task<IEnumerable<RepositorioRevelanteDTO>> ListarPorRelevanciaAsync(string nome, bool asc)
        {
            var repositorios = await _client.BuscarAsync(nome);

            if (!repositorios.Any()) return Enumerable.Empty<RepositorioRevelanteDTO>();

            var repositoriosRelevantes = repositorios
                .Select(repo => RepositorioMapper.ToRelevanteDTO(repo, _relevanciaService.Calcular(repo)));

            return OrdenarPorRelevancia(repositoriosRelevantes, asc);
        }

        private static IEnumerable<RepositorioRevelanteDTO> OrdenarPorRelevancia(IEnumerable<RepositorioRevelanteDTO> repositorios, bool asc)
        {
            return asc
                ? repositorios.OrderBy(r => r.Relevancia)
                : repositorios.OrderByDescending(r => r.Relevancia);
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
