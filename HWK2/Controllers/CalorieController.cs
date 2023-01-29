using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace CalorieRestAPI.Controllers
{
    [ApiController]// Indicates that this class is an API controller
    [Route("[controller]")] // Defines the routing template for the controller
    public class CalorieController : ControllerBase
    {
        private readonly ILogger<CalorieController> _logger;

        // constructor for dependency injection of ILogger
        public CalorieController(ILogger<CalorieController> logger)
        {
            _logger = logger;
        }

        // GET request to retrieve all calories
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Calorie>))]
        public IActionResult GetItems()
        {
            // Returns a HTTP 200 OK response with the list of calories
            return Ok(CalorieRepository.getInstance().getCalories());
        }

        // GET request to retrieve a single calorie by ID
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Calorie))]
        [ProducesResponseType(404)]
        public IActionResult GetItem(int id)
        {
            Calorie calorie = CalorieRepository.getInstance().GetCalorie(id);

            if (calorie == null)
            {
                // If no calorie is found, returns a HTTP 404 Not Found response
                return NotFound();
            }
            else
            { 
                // Returns a HTTP 200 OK response with the calorie
                return Ok(calorie);
            }
        }

        // POST request to add a new calorie
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)] // Indicates that this endpoint returns a 400 Bad Request status code
        public IActionResult AddItem([FromBody] Calorie calorie)
        {
            if (calorie == null)
            {
                return BadRequest("Calorie is null");
            }
            bool result = CalorieRepository.getInstance().addCalorie(calorie);

            if (result)
            {
                return Ok("Successfully added");
            }
            else
            {
                return BadRequest("Calorie not added");
            }
        }

        // PUT request to update an existing calorie by ID
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]// Indicates that this endpoint returns a 404 Not Found status code
        public IActionResult EditItem(int id, [FromBody] Calorie calorie)
        {
            if (calorie == null)
            {
                return BadRequest("Calorie is null");
            }

            bool isUpdated = CalorieRepository.getInstance().editCalorie(id, calorie);

            if (!isUpdated)
            {
                return NotFound("No matching Calorie");
            }
            else
            {
                return Ok("Successfully Updated");
            }
        }

        // DELETE endpoint that deletes a specific calorie by id
        [HttpDelete("{id}")]
         public IActionResult DeleteItem(int id)
        { 
        
          bool deleted = CalorieRepository.getInstance().deleteCalorie(id);

           if (!deleted)
         {
              return NotFound("No matching id");
         }
        else
         {
             return Ok("Calorie deleted");
        }
        }



        //This endpoint allows the user to retrieve an analysis of the calorie data.
        [HttpGet("analyze")]
        [ProducesResponseType(200, Type = typeof(CalorieAnalysis))]
        public IActionResult AnalyzeData()
        {
           // The analysis is returned as a "CalorieAnalysis" object, which is then returned as a response to the client with a 200 status code.
            CalorieAnalysis analysis = CalorieRepository.getInstance().AnalyzeData();

            return Ok(analysis);

        }

    }

}
