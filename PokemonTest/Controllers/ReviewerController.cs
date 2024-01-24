using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonTest.Dto;
using PokemonTest.Interfaces;
using PokemonTest.Models;

namespace PokemonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet("api/reviewers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetAllReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetAllReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("api/reviewer/{id}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int id)
        {
            if (!_reviewerRepository.ReviewerExists(id))
                return NotFound();

            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("api/getPokemon/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon (int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);  
        }

        [HttpGet("{reviewerId}/review")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewer = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpPost("/createReviewer")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest("Reviewer data is null");

            var existingReviewer = _reviewerRepository.GetAllReviewers()
                .Any(r => r.FirstName.Trim().ToUpper() == reviewerCreate.FirstName.Trim().ToUpper() &&
                          r.LastName.Trim().ToUpper() == reviewerCreate.LastName.Trim().ToUpper());

            if (existingReviewer)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewer = _mapper.Map<Reviewer>(reviewerCreate);

            if (!_reviewerRepository.CreateReviewer(reviewer))
            {
                ModelState.AddModelError("", $"Something went wrong saving the reviewer " +
                                                                   $"{reviewer.FirstName} {reviewer.LastName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetReviewer), new { id = reviewer.Id }, reviewer);
        }

    }
}
