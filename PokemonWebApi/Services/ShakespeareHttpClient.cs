using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PokemonWebApi.Models;

namespace PokemonWebApi.Services
{
    public class ShakespeareHttpClient
    {
        private readonly HttpClient _httpClient;

        public ShakespeareHttpClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<Shakespeare> GetTranslate(string textToTranslate)
        {
            string result = await CallApiEndPoint(this._httpClient.BaseAddress, textToTranslate);

            JObject jsonObject = JObject.Parse(result);
            Shakespeare responseShakespeare = jsonObject.ToObject<Shakespeare>();
            return responseShakespeare;
        }

        public async Task<string> CallApiEndPoint(Uri requestUri, string textToTranslate)
        {
            var data = new
            {
                text = textToTranslate
            };
            
            var response = await _httpClient.PostAsJsonAsync(requestUri, data);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.Content.ToString(), inner: null, response.StatusCode);
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
