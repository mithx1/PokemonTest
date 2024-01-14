using AutoMapper;
using PokemonTest.Models;
using PokemonTest.Dto;

namespace PokemonTest.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
