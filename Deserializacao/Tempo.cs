using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TimeVersion.Deserializacao
{

    public class Root
    {
        [JsonPropertyName("coord")]
        public Coordenadas Coordenadas { get; set; }

        [JsonPropertyName("weather")]
        public List<Clima> Clima { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("main")]
        public Main Main { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibilidade { get; set; }

        [JsonPropertyName("wind")]
        public Vento Vento { get; set; }

        [JsonPropertyName("clouds")]
        public Nuvens Nuvens { get; set; }

        [JsonPropertyName("dt")]
        public long Dt { get; set; }

        [JsonPropertyName("sys")]
        public Sys Sys { get; set; }

        [JsonPropertyName("timezone")]
        public int FusoHorario { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("cod")]
        public int? Codigo { get; set; }
    }
}
