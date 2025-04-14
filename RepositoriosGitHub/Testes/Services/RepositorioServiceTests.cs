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
        private readonly Mock<IFavoritosStorage> _mockFavoritosStorage;
        private readonly RepositorioService _service;

        public RepositorioServiceTests()
        {
            _mockClient = new Mock<IGitHubClient>();
            _mockRelevancia = new Mock<IRelevanciaService>();
            _mockFavoritosStorage = new Mock<IFavoritosStorage>();
            _service = new RepositorioService(_mockClient.Object, _mockRelevancia.Object, _mockFavoritosStorage.Object);
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
            _mockClient.Verify(c => c.BuscarDoUsuario(meuUsuario), Times.Once);
        }

        [Fact]
        public async Task ListarDoUsuarioAsync_DeveLancarExcecao_QuandoGitHubClientFalhar()
        {
            // Arrange
            var usuario = "teste";
            var mensagemErro = "Erro no GitHub";
            _mockClient.Setup(c => c.BuscarDoUsuario(usuario))
                .ThrowsAsync(new Exception(mensagemErro));

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.ListarDoUsuarioAsync(usuario));
            excecao.Message.Should().Be(mensagemErro);
        }

        [Fact]
        public async Task ListarDoUsuarioAsync_DeveRetornarVazio_QuandoNaoExistiremRepositorios()
        {
            // Arrange
            var inexistenteUsuario = "JoaoVGStahlNaoExiste";
            _mockClient.Setup(c => c.BuscarDoUsuario(inexistenteUsuario))
                .ReturnsAsync(new List<Repositorio>());

            // Act
            var resultado = await _service.ListarDoUsuarioAsync(inexistenteUsuario);

            // Assert
            resultado.Should().BeEmpty();
            _mockClient.Verify(c => c.BuscarDoUsuario(inexistenteUsuario), Times.Once);
        }

        [Fact]
        public async Task ListarPorNomeAsync_DeveRetornarListaDeDTO()
        {
            // Arrange
            var repos = new List<Repositorio> { new() { Id = 2, Name = "repo" } };
            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(repos);

            // Act
            var resultado = await _service.ListarPorNomeAsync("repo");

            // Assert
            resultado.Should().HaveCount(1);
            resultado.First().Id.Should().Be(2);
            _mockClient.Verify(c => c.BuscarAsync("repo"), Times.Once);
        }

        [Fact]
        public async Task ListarPorNomeAsync_DeveLancarExcecao_QuandoGitHubClientFalhar()
        {
            // Arrange
            var nome = "teste";
            var mensagemErro = "Erro no GitHub";
            _mockClient.Setup(c => c.BuscarAsync(nome))
                .ThrowsAsync(new Exception(mensagemErro));

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.ListarPorNomeAsync(nome));
            excecao.Message.Should().Be(mensagemErro);
        }

        [Fact]
        public async Task ListarPorNomeAsync_DeveRetornarVazio_SeNenhumRepositorioEncontrado()
        {
            // Arrange
            _mockClient.Setup(c => c.BuscarAsync("repo"))
                .ReturnsAsync(new List<Repositorio>());

            // Act
            var resultado = await _service.ListarPorNomeAsync("repo");

            // Assert
            resultado.Should().BeEmpty();
            _mockClient.Verify(c => c.BuscarAsync("repo"), Times.Once);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_DeveRetornarOrdenadoCrescente()
        {
            // Arrange
            var repo1 = new Repositorio { Id = 1, StargazersCount = 1, ForksCount = 1, WatchersCount = 1 };
            var repo2 = new Repositorio { Id = 2, StargazersCount = 5, ForksCount = 5, WatchersCount = 5 };

            var repos = new List<Repositorio> { repo1, repo2 };

            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(repos);
            _mockRelevancia.Setup(r => r.Calcular(repo1)).Returns(10);
            _mockRelevancia.Setup(r => r.Calcular(repo2)).Returns(20);

            // Act
            var resultado = (await _service.ListarPorRelevanciaAsync("repo", asc: true)).ToList();

            // Assert
            _mockRelevancia.Verify(r => r.Calcular(repo1), Times.Once);
            _mockRelevancia.Verify(r => r.Calcular(repo2), Times.Once);
            resultado.Should().HaveCount(2);
            resultado.First().Id.Should().Be(1);
            resultado.Last().Id.Should().Be(2);
            _mockClient.Verify(c => c.BuscarAsync("repo"), Times.Once);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_DeveRetornarOrdenadoDecrescente()
        {
            // Arrange
            var repo1 = new Repositorio { Id = 1, StargazersCount = 1, ForksCount = 1, WatchersCount = 1 };
            var repo2 = new Repositorio { Id = 2, StargazersCount = 5, ForksCount = 5, WatchersCount = 5 };

            var repos = new List<Repositorio> { repo1, repo2 };

            _mockClient.Setup(c => c.BuscarAsync("repo")).ReturnsAsync(repos);
            _mockRelevancia.Setup(r => r.Calcular(repo1)).Returns(10);
            _mockRelevancia.Setup(r => r.Calcular(repo2)).Returns(20);

            // Act
            var resultado = (await _service.ListarPorRelevanciaAsync("repo", asc: false)).ToList();

            // Assert
            _mockRelevancia.Verify(r => r.Calcular(repo1), Times.Once);
            _mockRelevancia.Verify(r => r.Calcular(repo2), Times.Once);
            resultado.Should().HaveCount(2);
            resultado.First().Id.Should().Be(2);
            resultado.Last().Id.Should().Be(1);
            _mockClient.Verify(c => c.BuscarAsync("repo"), Times.Once);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_DeveLancarExcecao_QuandoGitHubClientFalhar()
        {
            // Arrange
            var nome = "teste";
            var mensagemErro = "Erro no GitHub";
            _mockClient.Setup(c => c.BuscarAsync(nome))
                .ThrowsAsync(new Exception(mensagemErro));

            // Act & Assert
            var excecao = await Assert.ThrowsAsync<Exception>(() => _service.ListarPorRelevanciaAsync(nome, true));
            excecao.Message.Should().Be(mensagemErro);
        }

        [Fact]
        public async Task ListarPorRelevanciaAsync_DeveRetornarListaVazia_SeNaoHouverRepositorios()
        {
            // Arrange
            _mockClient.Setup(c => c.BuscarAsync("repo"))
                .ReturnsAsync(new List<Repositorio>());

            // Act
            var resultado = await _service.ListarPorRelevanciaAsync("repo", asc: true);

            // Assert
            resultado.Should().BeEmpty();
            _mockClient.Verify(c => c.BuscarAsync("repo"), Times.Once);
        }
    }
}