using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CalorieRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalorieController : ControllerBase
    {
        private readonly ILogger<CalorieController> _logger;

        public CalorieController(ILogger<CalorieController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Calorie>))]
        public IActionResult GetItems()
        {
            return Ok(CalorieRepository.getInstance().getCalories());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Calorie))]
        [ProducesResponseType(404)]
        public IActionResult GetItem(int id)
        {
            Calorie calorie = CalorieRepository.getInstance().GetCalorie(id);

            if (calorie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(calorie);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

      


        [HttpGet("analyze")]
        [ProducesResponseType(200, Type = typeof(CalorieAnalysis))]
        public IActionResult AnalyzeData()
        {
            CalorieAnalysis analysis = CalorieRepository.getInstance().AnalyzeData();

            return Ok(analysis);

        }

    }

}
