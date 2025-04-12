using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;

namespace Testes.Services
{
    public class RelevanceServiceTests
    {
        private readonly IRelevanciaService _service;

        public RelevanceServiceTests()
        {
            _service = new RelevanciaService();
        }

        [Fact]
        public void Deve_Calcular_Corretamente_Quando_Valores_Estao_Abaixo_Do_Limite()
        {
            var repo = new Repositorio
            {
                StargazersCount = 10,
                ForksCount = 5,
                WatchersCount = 2
            };

            var resultado = _service.Calcular(repo);

            var esperado = 10 * RelevanciaConfig.PESO_STARS +
                           5 * RelevanciaConfig.PESO_FORKS +
                           2 * RelevanciaConfig.PESO_WATCHERS;

            resultado.Should().Be(esperado);
        }

        [Fact]
        public void Deve_Aplicar_Limite_Maximo_De_Estrelas()
        {
            var repo = new Repositorio
            {
                StargazersCount = (int)RelevanciaConfig.MAX_STARS + 100,
                ForksCount = 0,
                WatchersCount = 0
            };

            var resultado = _service.Calcular(repo);

            resultado.Should().Be(RelevanciaConfig.MAX_STARS * RelevanciaConfig.PESO_STARS);
        }

        [Fact]
        public void Deve_Aplicar_Limite_Maximo_De_Forks()
        {
            var repo = new Repositorio
            {
                StargazersCount = 0,
                ForksCount = (int)RelevanciaConfig.MAX_FORKS + 50,
                WatchersCount = 0
            };

            var resultado = _service.Calcular(repo);

            resultado.Should().Be(RelevanciaConfig.MAX_FORKS * RelevanciaConfig.PESO_FORKS);
        }

        [Fact]
        public void Deve_Aplicar_Limite_Maximo_De_Watchers()
        {
            var repo = new Repositorio
            {
                StargazersCount = 0,
                ForksCount = 0,
                WatchersCount = (int)RelevanciaConfig.MAX_WATCHERS + 99
            };

            var resultado = _service.Calcular(repo);

            resultado.Should().Be(RelevanciaConfig.MAX_WATCHERS * RelevanciaConfig.PESO_WATCHERS);
        }
    }

}
