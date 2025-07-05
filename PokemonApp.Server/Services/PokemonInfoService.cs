using Newtonsoft.Json;
using PokemonApp.Server.Exceptions;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models;

namespace PokemonApp.Server.Services
{
    public class PokemonInfoService : IPokemonInfoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PokemonInfoService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Pokemon> GetPokemonAsync(string identifier)
        {
            string? _baseUrl = _configuration["PokemonApi:BaseUrl"];
            var url = $"{_baseUrl}/{identifier}";

            var client = _httpClientFactory.CreateClient("HttpClient");
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