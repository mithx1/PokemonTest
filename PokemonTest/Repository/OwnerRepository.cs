using PokemonTest.Data;
using PokemonTest.Interfaces;
using PokemonTest.Models;

namespace PokemonTest.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Owner> GetAllOwners()
        {
            return _context.Owners.ToList();
        }

        public Owner GetOwner(int id)
        {
           return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokemonId)
        {
            return _context.PokemonOwners
                .Where(po=> po.Pokemon.Id == pokemonId)
                .Select(o => o.Owner).ToList();  
        }

        public ICollection<Pokemon> GetPokemonsOfAnOwner(int ownerId)
        {
            return _context.PokemonOwners
                .Where(po=> po.Owner.Id == ownerId)
                .Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int id)
        {
           return _context.Owners.Any(o => o.Id == id);
        }
    }
}
