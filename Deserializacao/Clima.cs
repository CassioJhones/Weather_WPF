using System.Text.Json.Serialization;

namespace TimeVersion.Deserializacao
{
    public class Clima
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("description")]
        public string Descricao { get; set; }

        [JsonPropertyName("icon")]
        public string Icone { get; set; }
    }
}
