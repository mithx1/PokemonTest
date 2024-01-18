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
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet("api/categories")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetAllCategories()
        {
            // Map the list of Category objects to a list of CategoryDto objects
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAllCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("api/categories/{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            // Map the Category object to a CategoryDto object
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }


        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            // Map the list of Pokemon objects to a list of PokemonDto objects
            var pokemons = _mapper.Map<List<PokemonDto>>(
                _categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons);
        }

        [HttpPost("api/categories")]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryToCreate)
        {
            // Check if the categoryToCreate object is null
            if (categoryToCreate == null)
                return BadRequest(ModelState);

            // Check if a category with the same name already exists
            var category = _categoryRepository.GetAllCategories()
                .FirstOrDefault(c => string.Equals(c.Name.Trim(), categoryToCreate.Name.Trim(), StringComparison.OrdinalIgnoreCase));

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the CategoryDto object to a Category object
            var categoryMap = _mapper.Map<Category>(categoryToCreate);

            try
            {
                // Create the category
                if (!_categoryRepository.CreateCategory(categoryMap))
                {
                    throw new Exception("Creating the category failed on save");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong while saving the category");
                return StatusCode(500, ModelState);
            }

            // Return the created category
            return CreatedAtAction(nameof(GetCategory), new { id = categoryMap.Id }, categoryMap);
        }

    }
}
