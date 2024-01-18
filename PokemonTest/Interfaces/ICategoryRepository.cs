using PokemonTest.Models;

namespace PokemonTest.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool Save();
    }
}
