using System.Text.Json.Serialization;

namespace TimeVersion.Modelo
{
    public class Nuvens
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }
}
