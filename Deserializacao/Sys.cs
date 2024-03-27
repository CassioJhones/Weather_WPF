using System.Text.Json.Serialization;

namespace TimeVersion.Deserializacao
{
    public class Sys
    {
        [JsonPropertyName("country")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EPaises Pais { get; set; }

        [JsonPropertyName("sunrise")]
        public long NascerDoSol { get; set; }

        [JsonPropertyName("sunset")]
        public long PorDoSol { get; set; }
    }
}
