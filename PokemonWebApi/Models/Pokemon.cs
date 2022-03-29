using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokemonWebApi.Models
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("flavor_text_entries")]
        public List<FlavorText> DescriptionList { get; set; }
    }

    public class FlavorText
    {
        [JsonProperty("flavor_text")]
        public string Description { get; set; }

        [JsonProperty("language")]

        public Language Language { get; set; }
    }

    public class Language
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}
