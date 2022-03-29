using Newtonsoft.Json;

namespace PokemonWebApi.Models
{
    public class Shakespeare
    {
        [JsonProperty("contents")]
        public Content Content { get; set; }
    }

    public class Content
    {
        [JsonProperty("translated")]
        public string Translated { get; set; }

        [JsonProperty("text")]
        public string Original { get; set; }
    }
}
