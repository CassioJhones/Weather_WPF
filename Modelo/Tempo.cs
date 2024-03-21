using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeVersion.Modelo
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

    public class Nuvens
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }

    public class Sys
    {
        [JsonPropertyName("country")]
        public string Pais { get; set; }

        [JsonPropertyName("sunrise")]
        public long NascerDoSol { get; set; }

        [JsonPropertyName("sunset")]
        public long PorDoSol { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("coord")]
        public  Coordenadas Coordenadas { get; set; }

        [JsonPropertyName("weather")]
        public List<Clima> Clima { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("main")]
        public  Main Main { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibilidade { get; set; }

        [JsonPropertyName("wind")]
        public  Vento Vento { get; set; }

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
