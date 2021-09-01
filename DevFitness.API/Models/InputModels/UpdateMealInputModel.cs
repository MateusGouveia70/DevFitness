using System;

namespace DevFitness.API.Models.InputModels
{
    public class UpdateMealInputModel
    {
        public string Description { get; private set; }
        public int Calories { get; set; }
        public DateTime Date { get; private set; }
    }
}
