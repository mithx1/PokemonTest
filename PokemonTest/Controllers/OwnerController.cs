using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonTest.Dto;
using PokemonTest.Interfaces;
using PokemonTest.Models;
using System.Net;

namespace PokemonTest.Controllers
{
    public class OwnerController : Controller
    {
        private IOwnerRepository _ownerRepository;
        private IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
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
       

    }
}
