using AutoMapper;
using PokemonTest.Models;
using PokemonTest.Dto;

namespace PokemonTest.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //TODO use reverse map instead of creating both maps
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto, Pokemon>();
            //TODO use reverse map instead of creating both maps
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            //TODO use reverse map instead of creating both maps
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            //TODO use reverse map instead of creating both maps
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();

            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
            
        }
    }
}
