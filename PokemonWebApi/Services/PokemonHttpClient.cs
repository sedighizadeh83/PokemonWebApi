using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PokemonWebApi.Models;

namespace PokemonWebApi.Services
{
    public class PokemonHttpClient
    {
        private readonly HttpClient _httpClient;

        public PokemonHttpClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<Pokemon> GetPokemon(string name)
        {
            string CompletePath = Path.Combine(this._httpClient.BaseAddress.ToString(), name);
            string result = await CallApiEndPoint(new Uri(CompletePath));

            JObject jsonObject = JObject.Parse(result);
            Pokemon responsePokemon = jsonObject.ToObject<Pokemon>();
            return responsePokemon;
        }

        public async Task<string> CallApiEndPoint(Uri requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.Content.ToString(), inner: null, response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
