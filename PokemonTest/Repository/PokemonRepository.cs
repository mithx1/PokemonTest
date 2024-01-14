using PokemonTest.Data;
using PokemonTest.Interfaces;
using PokemonTest.Models;

namespace PokemonTest.Repository
{
    public class PokemonRepository :IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Pokemon> GetAllPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }
    }
}
