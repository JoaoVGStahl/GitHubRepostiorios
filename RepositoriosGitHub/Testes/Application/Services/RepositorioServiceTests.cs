using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Testes.Application.Services
{
    public class RepositorioServiceTests
    {
        private readonly Mock<IGitHubClient> _gitHubClientMock;
        private readonly RepositorioService _service;

        public RepositorioServiceTests()
        {
            _gitHubClientMock = new Mock<IGitHubClient>();
            _service = new RepositorioService(_gitHubClientMock.Object);
        }

        [Theory]
        [InlineData(100, 50, 30, 100 * RelevanciaConfig.PESO_STARS + 50 * RelevanciaConfig.PESO_FORKS + 30 * RelevanciaConfig.PESO_WATCHERS)]
        [InlineData(500, 200, 100, 500 * RelevanciaConfig.PESO_STARS + 200 * RelevanciaConfig.PESO_FORKS + 100 * RelevanciaConfig.PESO_WATCHERS)]
        public void CalcularRelevancia_ComValoresAbaixoDoLimite_DeveCalcularCorretamente(
            int stars, int forks, int watchers, double expectedRelevancia)
        {
            // Arrange
            var repositorio = new Repositorio
            {
                StargazersCount = stars,
                ForksCount = forks,
                WatchersCount = watchers
            };

            // Act
            var relevancia = _service.CalcularRelevancia(repositorio);

            // Assert
            Assert.Equal(expectedRelevancia, relevancia);
        }

        [Theory]
        [InlineData(2000, 1500, 1200, RelevanciaConfig.MAX_STARS * RelevanciaConfig.PESO_STARS + 
                                      RelevanciaConfig.MAX_FORKS * RelevanciaConfig.PESO_FORKS + 
                                      RelevanciaConfig.MAX_WATCHERS * RelevanciaConfig.PESO_WATCHERS)]
        public void CalcularRelevancia_ComValoresAcimaDoLimite_DeveAplicarLimites(
            int stars, int forks, int watchers, double expectedRelevancia)
        {
            // Arrange
            var repositorio = new Repositorio
            {
                StargazersCount = stars,
                ForksCount = forks,
                WatchersCount = watchers
            };

            // Act
            var relevancia = _service.CalcularRelevancia(repositorio);

            // Assert
            Assert.Equal(expectedRelevancia, relevancia);
        }

        [Fact]
        public void CalcularRelevancia_ComValoresZerados_DeveRetornarZero()
        {
            // Arrange
            var repositorio = new Repositorio
            {
                StargazersCount = 0,
                ForksCount = 0,
                WatchersCount = 0
            };

            // Act
            var relevancia = _service.CalcularRelevancia(repositorio);

            // Assert
            Assert.Equal(0, relevancia);
        }

        [Theory]
        [InlineData(0, 100, 50, 0 * RelevanciaConfig.PESO_STARS + 100 * RelevanciaConfig.PESO_FORKS + 50 * RelevanciaConfig.PESO_WATCHERS)]
        [InlineData(100, 0, 50, 100 * RelevanciaConfig.PESO_STARS + 0 * RelevanciaConfig.PESO_FORKS + 50 * RelevanciaConfig.PESO_WATCHERS)]
        [InlineData(100, 50, 0, 100 * RelevanciaConfig.PESO_STARS + 50 * RelevanciaConfig.PESO_FORKS + 0 * RelevanciaConfig.PESO_WATCHERS)]
        public void CalcularRelevancia_ComValoresMistos_DeveCalcularCorretamente(
            int stars, int forks, int watchers, double expectedRelevancia)
        {
            // Arrange
            var repositorio = new Repositorio
            {
                StargazersCount = stars,
                ForksCount = forks,
                WatchersCount = watchers
            };

            // Act
            var relevancia = _service.CalcularRelevancia(repositorio);

            // Assert
            Assert.Equal(expectedRelevancia, relevancia);
        }

        [Fact]
        public async Task ListarDoUsuarioAsync_ComUsuarioExistente_DeveRetornarRepositorios()
        {
            // Arrange
            var nomeUsuario = "testuser";
            var repositorios = new List<Repositorio>
            {
                new Repositorio { Id = 1, Name = "Repo1", StargazersCount = 100, ForksCount = 50, WatchersCount = 30 },
                new Repositorio { Id = 2, Name = "Repo2", StargazersCount = 200, ForksCount = 100, WatchersCount = 60 }
            };

            _gitHubClientMock.Setup(x => x.BuscarDoUsuario(nomeUsuario))
                .ReturnsAsync(repositorios);

            // Act
            var result = await _service.ListarDoUsuarioAsync(nomeUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Repo1", result.First().Nome);
            Assert.Equal("Repo2", result.Last().Nome);
            _gitHubClientMock.Verify(x => x.BuscarDoUsuario(nomeUsuario), Times.Once);
        }

        [Fact]
        public async Task ListarDoUsuarioAsync_ComUsuarioInexistente_DeveRetornarListaVazia()
        {
            // Arrange
            var nomeUsuario = "usuarioinexistente";
            _gitHubClientMock.Setup(x => x.BuscarDoUsuario(nomeUsuario))
                .ReturnsAsync(Enumerable.Empty<Repositorio>());

            // Act
            var result = await _service.ListarDoUsuarioAsync(nomeUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _gitHubClientMock.Verify(x => x.BuscarDoUsuario(nomeUsuario), Times.Once);
        }

        [Fact]
        public async Task ListarPorNomeAsync_ComRepositoriosEncontrados_DeveRetornarRepositorios()
        {
            // Arrange
            var nomeRepositorio = "test";
            var repositorios = new List<Repositorio>
            {
                new Repositorio { Id = 1, Name = "test-repo1", StargazersCount = 100, ForksCount = 50, WatchersCount = 30 },
                new Repositorio { Id = 2, Name = "test-repo2", StargazersCount = 200, ForksCount = 100, WatchersCount = 60 }
            };

            _gitHubClientMock.Setup(x => x.BuscarAsync(nomeRepositorio))
                .ReturnsAsync(repositorios);

            // Act
            var result = await _service.ListarPorNomeAsync(nomeRepositorio);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("test-repo1", result.First().Nome);
            Assert.Equal("test-repo2", result.Last().Nome);
            _gitHubClientMock.Verify(x => x.BuscarAsync(nomeRepositorio), Times.Once);
        }

        [Fact]
        public async Task ListarPorNomeAsync_ComNenhumRepositorioEncontrado_DeveRetornarListaVazia()
        {
            // Arrange
            var nomeRepositorio = "repositorioinexistente";
            _gitHubClientMock.Setup(x => x.BuscarAsync(nomeRepositorio))
                .ReturnsAsync(Enumerable.Empty<Repositorio>());

            // Act
            var result = await _service.ListarPorNomeAsync(nomeRepositorio);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _gitHubClientMock.Verify(x => x.BuscarAsync(nomeRepositorio), Times.Once);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_ComRepositoriosEncontrados_DeveRetornarRepositoriosOrdenados()
        {
            // Arrange
            var nomeRepositorio = "test";
            var repositorios = new List<Repositorio>
            {
                new Repositorio { Id = 1, Name = "test-repo1", StargazersCount = 100, ForksCount = 50, WatchersCount = 30 },
                new Repositorio { Id = 2, Name = "test-repo2", StargazersCount = 200, ForksCount = 100, WatchersCount = 60 }
            };

            _gitHubClientMock.Setup(x => x.BuscarAsync(nomeRepositorio))
                .ReturnsAsync(repositorios);

            // Act
            var result = await _service.ListarPorRelevanciaAsync(nomeRepositorio, true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            
            // Verifica se está ordenado por relevância (crescente)
            var relevancias = result.Select(r => r.Relevancia).ToList();
            Assert.True(relevancias[0] <= relevancias[1]);
            
            _gitHubClientMock.Verify(x => x.BuscarAsync(nomeRepositorio), Times.Once);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_ComNenhumRepositorioEncontrado_DeveRetornarListaVazia()
        {
            // Arrange
            var nomeRepositorio = "repositorioinexistente";
            _gitHubClientMock.Setup(x => x.BuscarAsync(nomeRepositorio))
                .ReturnsAsync(Enumerable.Empty<Repositorio>());

            // Act
            var result = await _service.ListarPorRelevanciaAsync(nomeRepositorio, true);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _gitHubClientMock.Verify(x => x.BuscarAsync(nomeRepositorio), Times.Once);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_ComOrdemDecrescente_DeveRetornarRepositoriosOrdenados()
        {
            // Arrange
            var nomeRepositorio = "test";
            var repositorios = new List<Repositorio>
            {
                new Repositorio { Id = 1, Name = "test-repo1", StargazersCount = 100, ForksCount = 50, WatchersCount = 30 },
                new Repositorio { Id = 2, Name = "test-repo2", StargazersCount = 200, ForksCount = 100, WatchersCount = 60 }
            };

            _gitHubClientMock.Setup(x => x.BuscarAsync(nomeRepositorio))
                .ReturnsAsync(repositorios);

            // Act
            var result = await _service.ListarPorRelevanciaAsync(nomeRepositorio, false);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            
            // Verifica se está ordenado por relevância (decrescente)
            var relevancias = result.Select(r => r.Relevancia).ToList();
            Assert.True(relevancias[0] >= relevancias[1]);
            
            _gitHubClientMock.Verify(x => x.BuscarAsync(nomeRepositorio), Times.Once);
        }
    }
} 