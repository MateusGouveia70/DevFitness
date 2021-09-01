using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    [Route("api/users/{userId}[controller]")]
    public class MealsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            return Ok();
        }


        [HttpGet("{mealId}")]
        public IActionResult GetById(int userId, int mealId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateMeal(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            return Ok();
        }

        [HttpPut("{mealId}")]
        public IActionResult UpdateMeal(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            return NoContent();
        }

        [HttpDelete("mealId")]
        public IActionResult Delete(int userId, int mealId)
        {
            return NoContent();
        }

    }
}
