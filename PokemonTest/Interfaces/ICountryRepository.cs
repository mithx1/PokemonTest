using PokemonTest.Models;

namespace PokemonTest.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetAllCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);

    
    }
}
