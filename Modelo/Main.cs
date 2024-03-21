using System.Text.Json.Serialization;

namespace TimeVersion.Modelo
{
    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temperatura { get; set; }

        [JsonPropertyName("feels_like")]
        public double SensacaoTermica { get; set; }

        [JsonPropertyName("temp_min")]
        public double TemperaturaMinima { get; set; }

        [JsonPropertyName("temp_max")]
        public double TemperaturaMaxima { get; set; }

        /// <summary>
        /// Pressao recebida da API em hPa (HectoPascal)
        /// </summary>
        /// <value>
        /// Necessario Fazer a conversão para Atm ou Bar
        /// </value>
        [JsonPropertyName("pressure")]
        public int Pressao { get; set; }

        [JsonPropertyName("humidity")]
        public int Umidade { get; set; }

        [JsonPropertyName("sea_level")]
        public int NivelDoMar { get; set; }

        [JsonPropertyName("grnd_level")]
        public int NivelDoSolo { get; set; }
    }
}
