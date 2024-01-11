using PokemonTest.Data;
using PokemonTest.Models;

namespace PokemonTest
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.PokemonOwners.Any())
            {
                var pokemonOwners = new List<PokemonOwner>()
                {
                    new PokemonOwner()
                    {
                        Pokemon = new Pokemon()
                        {
                            Name = "Zephyr",
                            BirthDate = new DateTime(2000, 5, 15),
                            PokemonCategories = new List<PokemonCategory>()
                            {
                                new PokemonCategory { Category = new Category() { Name = "Air"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Zephyr",Text = "Zephyr is an extraordinary pokemon, known for its aerial prowess", Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Aria", LastName = "Skye" } },
                                new Review { Title="Zephyr", Text = "Zephyr gracefully navigates through the skies, a sight to behold", Rating = 4,
                                    Reviewer = new Reviewer(){ FirstName = "Breeze", LastName = "Windsor" } },
                                new Review { Title="Zephyr",Text = "Zephyr, the embodiment of freedom and elegance", Rating = 5,
                                    Reviewer = new Reviewer(){ FirstName = "Gale", LastName = "Breeze" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Skyler",
                            LastName = "Windsor",
                            Gym = "Celestial Gym",
                            Country = new Country()
                            {
                                Name = "Aero Isles"
                            }
                        }
                    },
                    // (Repeat the above structure for additional PokemonOwners)

                };
                dataContext.PokemonOwners.AddRange(pokemonOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
