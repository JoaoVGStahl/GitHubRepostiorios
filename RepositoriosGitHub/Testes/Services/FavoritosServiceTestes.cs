using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Testes.Services;

public class FavoritosServiceTests
{
    private readonly Mock<IFavoritosStorage> _mockStorage;
    private readonly FavoritosService _service;

    public FavoritosServiceTests()
    {
        _mockStorage = new Mock<IFavoritosStorage>();
        _service = new FavoritosService(_mockStorage.Object);
    }

    [Fact]
    public void Adicionar_DeveConverterParaEntidade_E_ChamarStorage()
    {
        // Arrange
        var dto = new RepositorioDTO { Id = 1, Nome = "Repo" };
        _mockStorage.Setup(s => s.Adicionar(It.IsAny<Repositorio>())).Verifiable();

        // Act
        _service.Adicionar(dto);

        // Assert
        _mockStorage.Verify(s => s.Adicionar(It.Is<Repositorio>(r =>
            r.Id == dto.Id &&
            r.Name == dto.Nome)),
            Times.Once);
    }

    [Fact]
    public void Adicionar_DeveLancarExcecao_QuandoStorageFalhar()
    {
        // Arrange
        var dto = new RepositorioDTO { Id = 1, Nome = "Repo" };
        var mensagemErro = "Erro ao adicionar favorito";
        _mockStorage.Setup(s => s.Adicionar(It.IsAny<Repositorio>()))
            .Throws(new Exception(mensagemErro));

        // Act & Assert
        var excecao = Assert.Throws<Exception>(() => _service.Adicionar(dto));
        excecao.Message.Should().Be(mensagemErro);
    }

    [Fact]
    public void ListarFavoritos_DeveRetornarDTOs_QuandoHouverFavoritos()
    {
        // Arrange
        var lista = new List<Repositorio>
        {
            new() { Id = 1, Name = "Repo 1" },
            new() { Id = 2, Name = "Repo 2" }
        };

        _mockStorage.Setup(s => s.ListarFavoritos()).Returns(lista);

        // Act
        var resultado = _service.ListarFavoritos().ToList();

        // Assert
        resultado.Should().HaveCount(2);
        resultado.First().Nome.Should().Be("Repo 1");
        resultado.Last().Nome.Should().Be("Repo 2");
        _mockStorage.Verify(s => s.ListarFavoritos(), Times.Once);
    }

    [Fact]
    public void ListarFavoritos_DeveRetornarListaVazia_QuandoNaoHouverFavoritos()
    {
        // Arrange
        _mockStorage.Setup(s => s.ListarFavoritos()).Returns(new List<Repositorio>());

        // Act
        var resultado = _service.ListarFavoritos();

        // Assert
        resultado.Should().BeEmpty();
        _mockStorage.Verify(s => s.ListarFavoritos(), Times.Once);
    }

    [Fact]
    public void ObterPorId_DeveRetornarDTO_SeExistir()
    {
        // Arrange
        var repo = new Repositorio { Id = 1, Name = "Repo X" };
        _mockStorage.Setup(s => s.ObterPorId(1)).Returns(repo);

        // Act
        var resultado = _service.ObterPorId(1);

        // Assert
        resultado.Should().NotBeNull();
        resultado!.Id.Should().Be(1);
        resultado.Nome.Should().Be("Repo X");
        _mockStorage.Verify(s => s.ObterPorId(1), Times.Once);
    }

    [Fact]
    public void ObterPorId_DeveRetornarNull_SeNaoExistir()
    {
        // Arrange
        _mockStorage.Setup(s => s.ObterPorId(999)).Returns((Repositorio?)null);

        // Act
        var resultado = _service.ObterPorId(999);

        // Assert
        resultado.Should().BeNull();
        _mockStorage.Verify(s => s.ObterPorId(999), Times.Once);
    }

    [Fact]
    public void Remover_DeveChamarStorageComId()
    {
        // Arrange
        var id = 123;
        _mockStorage.Setup(s => s.Remover(id)).Verifiable();

        // Act
        _service.Remover(id);

        // Assert
        _mockStorage.Verify(s => s.Remover(id), Times.Once);
    }

    [Fact]
    public void Remover_DeveLancarExcecao_QuandoStorageFalhar()
    {
        // Arrange
        var id = 123;
        var mensagemErro = "Erro ao remover favorito";
        _mockStorage.Setup(s => s.Remover(id)).Throws(new Exception(mensagemErro));

        // Act & Assert
        var excecao = Assert.Throws<Exception>(() => _service.Remover(id));
        excecao.Message.Should().Be(mensagemErro);
    }
}
