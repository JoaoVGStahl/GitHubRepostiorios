using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Services
{
    // Necessário criar um serviço para facilitar a manutenção e realização dos testes unitários.
    public class RelevanciaService : IRelevanciaService
    {
        public double Calcular(Repositorio item)
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
    }
}
