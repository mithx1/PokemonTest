using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonTest.Dto;
using PokemonTest.Interfaces;
using PokemonTest.Models;
using PokemonTest.Repository;

namespace PokemonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private ICountryRepository _repository;
        private IMapper _mapper;

        public CountryController(ICountryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/getAllCountries")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            // Map the list of Country objects to a list of CountryDto objects
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
            // Check if the country with the given ID exists
            if (!_repository.CountryExists(countryId))
                return NotFound();

            // Map the Country object to a CountryDto object
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
            // Map the Country object associated with the owner ID to a CountryDto object
            var country = _mapper.Map<CountryDto>(
                _repository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        [HttpPost("/createCountry")]
        [ProducesResponseType(201, Type = typeof(Country))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCountry([FromBody] CountryDto countryToCreate)
        {
            if (countryToCreate == null)
                return BadRequest(ModelState);

            var country = _repository
                .GetAllCountries()
                .Where(c => c.Name == countryToCreate.Name)
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", $"Country {countryToCreate.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryObj = _mapper.Map<Country>(countryToCreate);

            if (!_repository.CreateCountry(countryObj))
            {
                ModelState.AddModelError("", $"Something went wrong saving {countryObj.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
  
        }

    }
}
