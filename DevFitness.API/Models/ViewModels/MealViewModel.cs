using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Models.ViewModels
{
    public class MealViewModel
    {
        public MealViewModel(int id, string description, int calories, string userName, DateTime date)
        {
            Id = id;
            Description = description;
            Calories = calories;
            UserName = userName;
            Date = date;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public int Calories { get; private set; }
        public string UserName { get; private set; }
        public DateTime Date { get; private set; }
    }
}
