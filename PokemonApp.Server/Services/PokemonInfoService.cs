using Newtonsoft.Json;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models;

namespace PokemonApp.Server.Services
{
    public class PokemonInfoService : IPokemonInfoService
    {
        public async Task<Pokemon> GetPokemonAsync(string query)
        {
            var url = $"https://pokeapi.co/api/v2/pokemon/{query}";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await client.GetAsync(url).ConfigureAwait(false);

            httpResponseMessage.EnsureSuccessStatusCode();

            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(jsonString);

            return pokemon;
        }
    }
}