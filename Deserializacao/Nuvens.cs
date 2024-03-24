using System.Text.Json.Serialization;

namespace TimeVersion.Deserializacao
{
    public class Nuvens
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }
}
