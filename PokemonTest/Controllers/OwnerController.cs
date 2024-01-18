using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonTest.Dto;
using PokemonTest.Interfaces;
using PokemonTest.Models;
using System.Net;

namespace PokemonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private IOwnerRepository _ownerRepository;
        private IMapper _mapper;
        private ICountryRepository _countryRepository;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper, ICountryRepository countryRepository)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }
        [HttpGet("api/owners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetAllOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetAllOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("api/owner/{id}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int id)
        {
            if (!_ownerRepository.OwnerExists(id))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId) 
        { 
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonsOfAnOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }
        [HttpPost("api/owner")]
        [ProducesResponseType(201, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateOwner([FromQuery] int countryId,[FromBody] OwnerDto ownerToCreate)
        {
            if (ownerToCreate == null)
                return BadRequest(ModelState);

            var existingOwner = _ownerRepository.GetAllOwners()
                .FirstOrDefault(c => c.FirstName.Trim().ToUpper() == ownerToCreate.FirstName.Trim().ToUpper() &&
                                     c.LastName.Trim().ToUpper() == ownerToCreate.LastName.Trim().ToUpper());

            if (existingOwner != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode (422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerObj = _mapper.Map<Owner>(ownerToCreate);

            var country = _countryRepository.GetCountry(countryId);
            if (country == null)
            {
                throw new Exception("Country not found");
            }

            ownerObj.Country = country;


            if (!_ownerRepository.CreateOwner(ownerObj))
            {
                ModelState.AddModelError("", $"Something went wrong saving {ownerObj.FirstName} {ownerObj.LastName}");
                return StatusCode(500, ModelState);
            }

            return Ok(ownerObj);
        }
       

    }
}
