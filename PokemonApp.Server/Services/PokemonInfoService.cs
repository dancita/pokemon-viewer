using Newtonsoft.Json;
using PokemonApp.Server.Exceptions;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models;

namespace PokemonApp.Server.Services
{
    public class PokemonInfoService : IPokemonInfoService
    {
        public async Task<Pokemon> GetPokemonAsync(string identifier)
        {
            var url = $"https://pokeapi.co/api/v2/pokemon/{identifier}";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await client.GetAsync(url).ConfigureAwait(false);
                
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new PokemonInfoException(httpResponseMessage.StatusCode);
            }
            var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Pokemon>(jsonString);
        }
    }
}