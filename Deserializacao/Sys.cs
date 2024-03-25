﻿using System.Text.Json.Serialization;

namespace TimeVersion.Deserializacao
{
    public class Sys
    {
        [JsonPropertyName("country")]
        public string Pais { get; set; }

        [JsonPropertyName("sunrise")]
        public long NascerDoSol { get; set; }

        [JsonPropertyName("sunset")]
        public long PorDoSol { get; set; }
    }
}