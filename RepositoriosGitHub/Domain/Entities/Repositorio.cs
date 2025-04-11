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
    }

}
