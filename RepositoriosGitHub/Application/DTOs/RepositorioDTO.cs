namespace Application.DTOs
{
    public class RepositorioDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Url { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public int Stars { get; set; }
        public int Forks { get; set; }
        public int Watchers { get; set; }
        public int Issues { get; set; }
        public string? Linguagem { get; set; }
    }
}
