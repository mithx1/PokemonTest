using PokemonTest.Models;

namespace PokemonTest.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetAllPokemons();
    }
}
