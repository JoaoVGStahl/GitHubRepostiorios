using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Repositorio
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("html_url")]
        public string Url { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int Stars { get; set; }

        [JsonPropertyName("forks_count")]
        public int Forks { get; set; }

        [JsonPropertyName("watchers_count")]
        public int Watchers { get; set; }

        public double Relevancia { get; set; }
    }

}
