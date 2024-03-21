using System.Text.Json.Serialization;

namespace TimeVersion.Modelo
{
    public class Vento
    {
        /// <summary>
        /// Velocidade do Vento em m/s
        /// </summary>
        /// <value>
        /// Converter para Km/Hr
        /// </value>
        [JsonPropertyName("speed")]
        public double Velocidade { get; set; }

        [JsonPropertyName("deg")]
        public int Grau { get; set; }

        [JsonPropertyName("gust")]
        public double Rajadas { get; set; }
    }
}
