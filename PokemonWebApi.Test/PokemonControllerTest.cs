using System.Threading.Tasks;
using PokemonWebApi.Controllers;
using PokemonWebApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Http;
using Xunit;
using System;

namespace PokemonWebApi.Test
{
    public class PokemonControllerTest
    {
        private readonly PokemonController _controller;
        private readonly PokemonHttpClient _pokemonService;
        private readonly ShakespeareHttpClient _shakespeareService;
        private readonly HttpClient _pokemonHttpClient;
        private readonly HttpClient _shakespeareHttpClient;

        public PokemonControllerTest()
        {
            _pokemonHttpClient = new HttpClient();
            _pokemonHttpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species");
            _pokemonHttpClient.Timeout = new TimeSpan(0, 0, 60);
            _pokemonHttpClient.DefaultRequestHeaders.Clear();

            _shakespeareHttpClient = new HttpClient();
            _shakespeareHttpClient.BaseAddress = new System.Uri("https://api.funtranslations.com/translate/shakespeare.json");
            _shakespeareHttpClient.Timeout = new TimeSpan(0, 0, 60);
            _shakespeareHttpClient.DefaultRequestHeaders.Clear();

            _pokemonService = new PokemonHttpClient(_pokemonHttpClient);
            _shakespeareService = new ShakespeareHttpClient(_shakespeareHttpClient);

            _controller = new PokemonController(_pokemonService, _shakespeareService);
        }

        [Fact]
        public async Task GetPokemonDescriptionByName_NotExistingPokemon_ThrowsHttpRequestException()
        {
            /// Arrange
            var notExistingPokemon = "NotExistingPokemon";

            /// Act
            /// Assert
            Assert.ThrowsAsync<HttpRequestException>(async () => await _controller.GetPokemonDescriptionByName(notExistingPokemon));
        }

        [Fact]
        public async Task GetPokemonDescriptionByName_ExistingPokemon_ReturnsOKResponse()
        {
            /// Arrange
            var existingPokemon = "charizard";

            /// Act
            var result = await _controller.GetPokemonDescriptionByName(existingPokemon);

            /// Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPokemonDescriptionByName_ExistingPokemon_ReturnsOnePokemon()
        {
            /// Arrange
            var existingPokemon = "charizard";

            /// Act
            var okResult = await _controller.GetPokemonDescriptionByName(existingPokemon) as OkObjectResult;

            /// Assert
            var item = Assert.IsType<DTO.Pokemon>(okResult.Value);
            Assert.NotNull(item);
        }
    }
}