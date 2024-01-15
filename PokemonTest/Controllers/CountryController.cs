using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonTest.Dto;
using PokemonTest.Interfaces;
using PokemonTest.Models;
using PokemonTest.Repository;

namespace PokemonTest.Controllers
{
    public class CountryController : Controller
    {
        private ICountryRepository _repository;
        private IMapper _mapper;

        public CountryController(ICountryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_repository.GetAllCountries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_repository.CountryExists(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_repository.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(
                _repository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }





    }
}
