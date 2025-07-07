using Microsoft.EntityFrameworkCore;
using PokemonApp.Server.Models;
using Type = PokemonApp.Server.Models.Type;

namespace PokemonApp.Server.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Pokemon> Pokemon { get; set; }

        public DbSet<Type> Type { get; set; }

        public DbSet<Ability> Ability { get; set; } 

        public DbSet<PokemonType> PokemonTypes { get; set; } 

        public DbSet<PokemonAbility> PokemonAbilities { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.Property(p => p.Name)
                    .HasColumnType("varchar(12)")
                    .IsRequired(); 

                entity.Property(p => p.FrontDefaultImage)
                    .HasColumnType("nvarchar(2048)");
            });

            modelBuilder.Entity<Ability>(entity =>
            {
                entity.Property(a => a.Name)
                    .HasColumnType("varchar(50)");

                entity.Property(a => a.Url)
                    .HasColumnType("nvarchar(2048)");
            });

            modelBuilder.Entity<PokemonType>()
                .HasKey(pt => new { pt.PokemonId, pt.TypeId });

            modelBuilder.Entity<PokemonType>()
                .HasOne(pt => pt.Pokemon)
                .WithMany(p => p.Types)
                .HasForeignKey(pt => pt.PokemonId);

            modelBuilder.Entity<PokemonType>()
                .HasOne(pt => pt.Type)
                .WithMany(t => t.PokemonTypes)
                .HasForeignKey(pt => pt.TypeId);

            modelBuilder.Entity<PokemonAbility>()
                .HasKey(pa => new { pa.PokemonId, pa.AbilityId });

            modelBuilder.Entity<PokemonAbility>()
                .HasOne(pa => pa.Pokemon)
                .WithMany(p => p.Abilities)
                .HasForeignKey(pa => pa.PokemonId);

            modelBuilder.Entity<PokemonAbility>()
                .HasOne(pa => pa.Ability)
                .WithMany(a => a.PokemonAbilities)
                .HasForeignKey(pa => pa.AbilityId);

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(t => t.Name)
                    .HasColumnType("varchar(50)");

                entity.Property(t => t.Url)
                    .HasColumnType("nvarchar(2048)");

                entity.HasData(
                    new Type { Id = 1, Name = "Normal", Url = "https://pokeapi.co/api/v2/type/1/" },
                    new Type { Id = 2, Name = "Fighting", Url = "https://pokeapi.co/api/v2/type/2/" },
                    new Type { Id = 3, Name = "Flying", Url = "https://pokeapi.co/api/v2/type/3/" },
                    new Type { Id = 4, Name = "Poison", Url = "https://pokeapi.co/api/v2/type/4/" },
                    new Type { Id = 5, Name = "Ground", Url = "https://pokeapi.co/api/v2/type/5/" },
                    new Type { Id = 6, Name = "Rock", Url = "https://pokeapi.co/api/v2/type/6/" },
                    new Type { Id = 7, Name = "Bug", Url = "https://pokeapi.co/api/v2/type/7/" },
                    new Type { Id = 8, Name = "Ghost", Url = "https://pokeapi.co/api/v2/type/8/" },
                    new Type { Id = 9, Name = "Steel", Url = "https://pokeapi.co/api/v2/type/9/" },
                    new Type { Id = 10, Name = "Fire", Url = "https://pokeapi.co/api/v2/type/10/" },
                    new Type { Id = 11, Name = "Water", Url = "https://pokeapi.co/api/v2/type/11/" },
                    new Type { Id = 12, Name = "Grass", Url = "https://pokeapi.co/api/v2/type/12/" },
                    new Type { Id = 13, Name = "Electric", Url = "https://pokeapi.co/api/v2/type/13/" },
                    new Type { Id = 14, Name = "Psychic", Url = "https://pokeapi.co/api/v2/type/14/" },
                    new Type { Id = 15, Name = "Ice", Url = "https://pokeapi.co/api/v2/type/15/" },
                    new Type { Id = 16, Name = "Dragon", Url = "https://pokeapi.co/api/v2/type/16/" },
                    new Type { Id = 17, Name = "Dark", Url = "https://pokeapi.co/api/v2/type/17/" },
                    new Type { Id = 18, Name = "Fairy", Url = "https://pokeapi.co/api/v2/type/18/" }
                );
            });
        }
    }
}