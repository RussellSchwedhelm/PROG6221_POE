using System;
namespace PROG6221_POE
{
	public class Ingredient
	{
        // Initiallizing all properties with given arguments 
        public Ingredient(string name, string unitOfMeasurement, double quantity, int calories, string foodGroup)
        {
            this.Name = name;
            this.UnitOfMeasurement = unitOfMeasurement;
            this.Quantity = quantity;
            this.Calories = calories;
            this.FoodGroup = foodGroup;
        }

//----------------------------------------------------------------------------\\

        private string name; // Private field representing the ingredient's name
        private string unitOfMeasurement; // Private field representing the ingredient's unit of measurement
        private double quantity; // Private field representing the ingredient's quantity
        private int calories; // Private field representing the ingredient's calories
        private string foodGroup; // Private field representing the ingredient's food group

        //----------------------------------------------------------------------------\\

        // public getter/setter for 'name' variable through 'Name' property   
        public string Name { get => name; set => name = value; }
        // public getter/setter for 'unitOfMeasurement' variable through 'UnitOfMeasurement' property
        public string UnitOfMeasurement { get => unitOfMeasurement; set => unitOfMeasurement = value; }
        // public getter/setter for 'quantity' variable through 'Quantity' property
        public double Quantity { get => quantity; set => quantity = value; }
        // public getter/setter for 'calories' variable through 'Calories' property
        public int Calories { get => calories; set => calories = value; }
        // public getter/setter for 'foodGroup' variable through 'FoodGroup' property
        public string FoodGroup { get => foodGroup; set => foodGroup = value; }

        //----------------------------------------------------------------------------\\
    }
}
//----------------------------------------------------------------------------\\

