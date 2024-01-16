using PokemonTest.Data;
using PokemonTest.Dto;
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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.Owners.SingleOrDefault(a => a.Id == ownerId);
            var pokemonCategoryEntity = _context.Categories.SingleOrDefault(a => a.Id == categoryId);

            if (pokemonOwnerEntity == null || pokemonCategoryEntity == null)
            {
                return false;
            }

            var pokemonOwner = new PokemonOwner
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };

            var pokemonCategory = new PokemonCategory
            {
                Category = pokemonCategoryEntity,
                Pokemon = pokemon
            };

            _context.AddRange(new object[] { pokemonOwner, pokemonCategory, pokemon });

            return Save();
        }


        public ICollection<Pokemon> GetAllPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == id).FirstOrDefault();

            if(review == null)
            {
                return 0;
            }
            return ((decimal)review.Rating);
         
        }

        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate)
        {
           return GetAllPokemons().Where(p => p.Name.Trim().ToUpper() == pokemonCreate.Name.Trim().ToUpper()).FirstOrDefault();
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }
    }
}
