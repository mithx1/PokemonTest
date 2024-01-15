using PokemonTest.Models;

namespace PokemonTest.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetAllOwners();
        Owner GetOwner(int id);
        ICollection<Owner> GetOwnerOfAPokemon(int pokemonId);
        ICollection<Pokemon> GetPokemonsOfAnOwner(int ownerId);
        bool OwnerExists(int id);
    
    }
}
