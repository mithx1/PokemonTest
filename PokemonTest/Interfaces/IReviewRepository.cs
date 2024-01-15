using PokemonTest.Models;

namespace PokemonTest.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewsOfAPokemon(int pokemonId);
        bool ReviewExists(int id);
    }
}
