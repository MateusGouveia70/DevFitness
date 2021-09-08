using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Controllers
{
    [Route("api/users/{userId}[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;

        public MealsController(DevFitnessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var allMeals = _dbContext.Meals
                .Include(m => m.User)
                .Where(m => m.UserId == userId && m.Active);

            if (allMeals == null) return NotFound();

            var allMealViewModels = allMeals
                .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.User.FullName, m.Date))
                .ToList();

            return Ok(allMealViewModels);
        }


        [HttpGet("{mealId}")]
        public IActionResult GetById(int userId, int mealId)
        {
            var meal = _dbContext.Meals
                .Include(m => m.User)
                .SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if (meal == null) return NotFound();

            var mealViewModel = new MealViewModel(meal.Id, meal.Description, meal.Calories, meal.User.FullName, meal.Date);

            return Ok(mealViewModel);
        }

        [HttpPost]
        public IActionResult CreateMeal(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            var meal = new Meal(inputModel.Description, inputModel.Calories, inputModel.Date, userId);
             
            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { userId = userId, mealId = meal.Id }, inputModel);
        }

        [HttpPut("{mealId}")]
        public IActionResult UpdateMeal(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.Id == mealId && m.UserId == userId);

            if (meal == null) return NotFound();

            meal.Update(inputModel.Description, inputModel.Calories, inputModel.Date);

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("mealId")]
        public IActionResult Delete(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.Id == mealId && m.UserId == userId);

            if (meal == null) NotFound();

            meal.Deactivate();

            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}
