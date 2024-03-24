using System.Text.Json.Serialization;

namespace TimeVersion.Deserializacao
{
    public class Coordenadas
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }
}
