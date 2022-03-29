using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using PokemonWebApi.Services;
using System.Threading.Tasks;

namespace PokemonWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonHttpClient _pokemonHttpClient;
        private readonly ShakespeareHttpClient _shakespeareHttpClient;

        public PokemonController(PokemonHttpClient pokemonHttpClient, ShakespeareHttpClient shakespeareHttpClient)
        {
            this._pokemonHttpClient = pokemonHttpClient;
            this._shakespeareHttpClient = shakespeareHttpClient;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetPokemonByName(string name)
        {
            var pokemonModel = await _pokemonHttpClient.GetPokemon(name);

            if (pokemonModel == null)
            {
                return NotFound();
            }

            string description = String.Join('\n', pokemonModel.DescriptionList.Where(x => x.Language.name == "en").Select(x => x.Description).FirstOrDefault());

            var shakespeareModel = await _shakespeareHttpClient.GetTranslate(description);

            var pokemonDTO = new DTO.Pokemon()
            {
                Id = pokemonModel.Id,
                Name = pokemonModel.Name,
                Description = shakespeareModel == null ? description : shakespeareModel.Content.Translated
            };

            return Ok(pokemonDTO);
        }
    }
}
