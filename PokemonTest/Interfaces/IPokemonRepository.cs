using PokemonTest.Models;

namespace PokemonTest.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetAllPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int id);
        bool PokemonExists(int id);
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool Save();
    }
}
