using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CalorieRestAPIMySQL.Repositories;
using CalorieRestAPIMySQL.Data;
using Microsoft.EntityFrameworkCore;

namespace CalorieRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalorieController : ControllerBase
    {
        private readonly ILogger<CalorieController> _logger;
        private readonly ICalorieRepository _calorieRepository;

        public CalorieController(ILogger<CalorieController> logger, ICalorieRepository calorieRepository)
        {
            _logger = logger;
            _calorieRepository = calorieRepository;
        }

        /// <summary>
        /// Retrieves a list of all Calorie objects from the repository.
        /// </summary>
        /// <returns>A list of Calorie objects.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CalorieClass>))]
        public IActionResult GetCalories()
        {
            return Ok(_calorieRepository.GetCalories());
        }

        /// <summary>
        /// Retrieves a specific Calorie object based on the provided id.
        /// </summary>
        /// <param name="id">The id of the Calorie object to retrieve.</param>
        /// <returns>The Calorie object with the specified id, or a 404 error if not found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CalorieClass))]
        [ProducesResponseType(404)]
        public IActionResult GetCalorie(int id)
        {
            CalorieClass calorie = _calorieRepository.GetCalorie(id);

            if (calorie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(calorie);
            }
        }

        /// <summary>
        /// Adds a new Calorie object to the repository.
        /// </summary>
        /// <param name="calorie">The Calorie object to add to the repository.</param>
        /// <returns>A 200 status code if the Calorie object was successfully added, or a 400 error if the provided Calorie object is invalid.</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCalorie([FromBody] CalorieClass calorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = _calorieRepository.CreateCalorie(calorie);

            if (result)
            {
                return CreatedAtAction(nameof(GetCalorie), new { id = calorie.Id }, calorie);
            }
            else
            {
                return BadRequest("Failed to create Calorie");
            }
        }

        /// <summary>
        /// Updates an existing Calorie object in the repository.
        /// </summary>
        /// <param name="id">The id of the Calorie object to update.</param>
        /// <param name="calorie">The updated Calorie object.</param>
        /// <returns>A 200 status code if the Calorie object was successfully updated, a 400 error if the provided Calorie object is invalid,
        /// or a 404 error if the Calorie object with the specified id was not found in the repository.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCalorie(int id, [FromBody] CalorieClass calorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isExists = _calorieRepository.CalorieExists(id);

            if (!isExists)
            {
                return NotFound();
            }

            calorie.Id = id;

            bool result = _calorieRepository.UpdateCalorie(calorie);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Failed to update Calorie");
            }
        }

        /// <summary>
        /// Delete a calorie item by its id
        /// </summary>
        /// <param name="id">Id of the calorie item to delete</param>
        /// <returns>The deleted calorie item if it was successful, 404 Not Found if the calorie item was not found,
        /// and a 200 OK status code if the deletion was successful but the calorie item was not found</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCalorie(int id)
        {
            CalorieClass calorie = _calorieRepository.GetCalorie(id);

            if (calorie == null)
            {
                return NotFound();
            }

            bool success = _calorieRepository.DeleteCalorie(id);

            if (!success)
            {
                return StatusCode(200);
            }

            return Ok(calorie);

        }

        /// <summary>
        /// Get calorie analysis data
        /// </summary>
        /// <returns>The calorie analysis data</returns>
        [HttpGet("analysis")]
        public IActionResult GetCalorieAnalysis()
        {
            var result = _calorieRepository.GetCalorieAnalysis();
            return Ok(result);


        }
    }
}
    