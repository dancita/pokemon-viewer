namespace PokemonApp.Server.Models
{
    public abstract class BaseAPIEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}