using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Testes.Services
{
    public class RepositorioServiceTests
    {
        private readonly Mock<IGitHubClient> _mockClient;
        private readonly Mock<IRelevanciaService> _mockRelevancia;
        private readonly RepositorioService _service;

        public RepositorioServiceTests()
        {
            _mockClient = new Mock<IGitHubClient>();
            _mockRelevancia = new Mock<IRelevanciaService>();
            _service = new RepositorioService(_mockClient.Object, _mockRelevancia.Object);
        }

        [Fact]
        public async Task ListarDoUsuarioAsync_DeveRetornarListaDeDTO_QuandoExistiremRepositorios()
        {
            // Arrange
            var meuUsuario = "JoaoVGStahl";
            var repositorios = new List<Repositorio>
            {
                new() { Id = 1, Name = "Repo 1", StargazersCount = 5, ForksCount = 2, WatchersCount = 3 }
            };

            _mockClient.Setup(c => c.BuscarDoUsuario(meuUsuario)).ReturnsAsync(repositorios);

            // Act
            var resultado = await _service.ListarDoUsuarioAsync(meuUsuario);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(1);
            resultado.First().Nome.Should().Be("Repo 1");
        }

        [Fact]
        public async Task ListarDoUsuarioAsync_DeveRetornarVazio_QuandoNaoExistiremRepositorios()
        {
            var inexistenteUsuario = "JoaoVGStahlNaoExiste";
            _mockClient.Setup(c => c.BuscarDoUsuario(inexistenteUsuario)).ReturnsAsync(new List<Repositorio>());

            var resultado = await _service.ListarDoUsuarioAsync(inexistenteUsuario);

            resultado.Should().BeEmpty();
        }

        [Fact]
        public async Task ListarPorNomeAsync_DeveRetornarListaDeDTO()
        {
            var repos = new List<Repositorio> { new() { Id = 2, Name = "repo" } };
            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(repos);

            var resultado = await _service.ListarPorNomeAsync("repo");

            resultado.Should().HaveCount(1);
            resultado.First().Id.Should().Be(2);
        }

        [Fact]
        public async Task ListarPorNomeAsync_DeveRetornarVazio_SeNenhumRepositorioEncontrado()
        {
            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(new List<Repositorio>());

            var resultado = await _service.ListarPorNomeAsync("repo");

            resultado.Should().BeEmpty();
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_DeveRetornarOrdenadoCrescente()
        {
            var repo1 = new Repositorio { Id = 1, StargazersCount = 1, ForksCount = 1, WatchersCount = 1 };
            var repo2 = new Repositorio { Id = 2, StargazersCount = 5, ForksCount = 5, WatchersCount = 5 };

            var repos = new List<Repositorio> { repo1, repo2 };

            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(repos);

            _mockRelevancia.Setup(r => r.Calcular(repo1)).Returns(10);
            _mockRelevancia.Setup(r => r.Calcular(repo2)).Returns(20);

            var resultado = (await _service.ListarPorRelevanciaAsync("repo", asc: true)).ToList();

            _mockRelevancia.Verify(r => r.Calcular(repo1), Times.Once);
            _mockRelevancia.Verify(r => r.Calcular(repo2), Times.Once);
            resultado.Should().HaveCount(2);
            resultado.First().Id.Should().Be(1);
            resultado.Last().Id.Should().Be(2);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_DeveRetornarOrdenadoDecrescente()
        {
            var repo1 = new Repositorio { Id = 1, StargazersCount = 1, ForksCount = 1, WatchersCount = 1 };
            var repo2 = new Repositorio { Id = 2, StargazersCount = 5, ForksCount = 5, WatchersCount = 5 };

            var repos = new List<Repositorio> { repo1, repo2 };

            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(repos);

            _mockRelevancia.Setup(r => r.Calcular(repo1)).Returns(10);
            _mockRelevancia.Setup(r => r.Calcular(repo2)).Returns(20);

            var resultado = (await _service.ListarPorRelevanciaAsync("repo", asc: false)).ToList();

            _mockRelevancia.Verify(r => r.Calcular(repo1), Times.Once);
            _mockRelevancia.Verify(r => r.Calcular(repo2), Times.Once);
            resultado.Should().HaveCount(2);
            resultado.First().Id.Should().Be(2);
            resultado.Last().Id.Should().Be(1);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_DeveRetornarListaVazia_SeNaoHouverRepositorios()
        {
            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(new List<Repositorio>());

            var resultado = await _service.ListarPorRelevanciaAsync("repo", asc: true);

            resultado.Should().BeEmpty();
        }
    }
}