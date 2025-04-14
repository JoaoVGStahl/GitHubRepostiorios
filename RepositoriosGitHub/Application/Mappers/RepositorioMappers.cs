using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class RepositorioMapper
    {
        public static RepositorioDTO ToDTO(Repositorio entity, bool favorito = true)
        {
            return new RepositorioDTO
            {
                Id = entity.Id,
                Nome = entity.Name,
                Descricao = entity.Description,
                Stars = entity.StargazersCount,
                Forks = entity.ForksCount,
                Watchers = entity.WatchersCount,  
                Url = entity.HtmlUrl,
                DataCriacao = entity.CreatedAt,
                DataUltimaAtualizacao = entity.UpdatedAt,
                Issues = entity.OpenIssuesCount,
                Linguagem = entity.Language,
                Favorito = favorito
            };
        }

        public static Repositorio ToEntity(RepositorioDTO dto)
        {
            return new Repositorio
            {
                Id = dto.Id,
                Name = dto.Nome,
                Description = dto.Descricao,
                StargazersCount = dto.Stars,
                ForksCount = dto.Forks,
                WatchersCount = dto.Watchers,
                HtmlUrl = dto.Url,
                CreatedAt = dto.DataCriacao,
                UpdatedAt = dto.DataUltimaAtualizacao,
                Language = dto.Linguagem,
                OpenIssuesCount = dto.Issues
            };
        }

        public static RepositorioRevelanteDTO ToRelevanteDTO(Repositorio entity, double relevancia)
        {
            return new RepositorioRevelanteDTO
            {
                Id = entity.Id,
                Nome = entity.Name,
                Descricao = entity.Description,
                Stars = entity.StargazersCount,
                Forks = entity.ForksCount,
                Watchers = entity.WatchersCount,
                Url = entity.HtmlUrl,
                DataCriacao = entity.CreatedAt,
                DataUltimaAtualizacao = entity.UpdatedAt,
                Issues = entity.OpenIssuesCount,
                Linguagem = entity.Language,
                Relevancia = relevancia
            };
        }
    }
}
