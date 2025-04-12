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

        // Act
        _service.Adicionar(dto);

        // Assert
        _mockStorage.Verify(s => s.Adicionar(It.Is<Repositorio>(r => r.Id == dto.Id && r.Name == dto.Nome)), Times.Once);
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
    }

    [Fact]
    public void ListarFavoritos_DeveRetornarListaVazia_QuandoNaoHouverFavoritos()
    {
        _mockStorage.Setup(s => s.ListarFavoritos()).Returns(new List<Repositorio>());

        var resultado = _service.ListarFavoritos();

        resultado.Should().BeEmpty();
    }

    [Fact]
    public void ObterPorId_DeveRetornarDTO_SeExistir()
    {
        var repo = new Repositorio { Id = 1, Name = "Repo X" };

        _mockStorage.Setup(s => s.ObterPorId(1)).Returns(repo);

        var resultado = _service.ObterPorId(1);

        resultado.Should().NotBeNull();
        resultado!.Id.Should().Be(1);
        resultado.Nome.Should().Be("Repo X");
    }

    [Fact]
    public void ObterPorId_DeveRetornarNull_SeNaoExistir()
    {
        _mockStorage.Setup(s => s.ObterPorId(999)).Returns((Repositorio?)null);

        var resultado = _service.ObterPorId(999);

        resultado.Should().BeNull();
    }

    [Fact]
    public void Remover_DeveChamarStorageComId()
    {
        var id = 123;

        _service.Remover(id);

        _mockStorage.Verify(s => s.Remover(id), Times.Once);
    }
}
