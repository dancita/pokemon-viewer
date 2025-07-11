﻿namespace PokemonApp.Server.Models
{
    public class PokemonRequest
    {
        public int Id { get; set; }

        public int PokemonApiId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}