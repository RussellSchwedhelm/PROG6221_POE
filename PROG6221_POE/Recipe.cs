using System;

namespace PROG6221_POE
{
    public delegate string RecipeActions(double input);

    public class Recipe
    {
        private List<Ingredient> ingredientsList = new List<Ingredient>();
        private List<string> stepsList = new List<string>();
        //----------------------------------------------------------------------------\\
        //Getters And Setters
        public List<Ingredient> IngredientsList { get => ingredientsList; set => ingredientsList = value; }
        public List<String> StepsList { get => stepsList; set => stepsList = value; }
        //----------------------------------------------------------------------------\\
        //This method adds an ingredient object to an array of ingredients
        public void addIngredient(string ingredientName, string unitOfMeasurement,
                                  double ingredientQuantity, int calories, string foodGroup)
        {
            // Create an instance of the Ingredient class based on user-provided parameters.
            Ingredient ingredientToAdd = new Ingredient(ingredientName, unitOfMeasurement,
                                                        ingredientQuantity, calories, foodGroup);

            // Add the newly created Ingredient object to the Ingredients list
            IngredientsList.Add(ingredientToAdd);
        }
        //----------------------------------------------------------------------------\\

        // This method adds a step to StepsList
        public void addStep(string step)
        {
            // Place the new "step" to the step list
            StepsList.Add(step);
        }

        //----------------------------------------------------------------------------\\

        // Method to display a recipe with ingredients and steps
        public string DisplayRecipe(double scale)
        {
            // Initialize strings to store the ingredients and steps
            string ingredientsToString = "Ingredients:";
            string stepsToString = "Steps:";
            string calories = "Recipe Calories: ";
            double totalCalories = 0;

            // Initialize a counter for the steps
            int stepCount = 1;

            // Iterate over the list of ingredients
            foreach (Ingredient ingredient in IngredientsList)
            {
                // Append the correct quantity and measurement for the scaled ingredient to the string
                ingredientsToString += "\n" + CorrectQuantityAndMeasurement(scale, ingredient.Quantity, ingredient.UnitOfMeasurement)
                    + " " + ingredient.Name + " [Food Group: " + ingredient.FoodGroup + "]";
                totalCalories += ingredient.Calories;
            }

            totalCalories *= scale;

            // Iterate over the list of steps
            foreach (string step in StepsList)
            {
                // Append the step number and text to the string
                stepsToString += "\n" + stepCount + ". " + stepsList.ElementAt(stepCount - 1);

                // Increment the step counter
                stepCount++;
            }

            calories += totalCalories;

            // Combine the strings for ingredients and steps
            string recipe = ingredientsToString + "\n\n" + stepsToString + "\n\n" + calories;

            // Return the combined recipe string
            return recipe;
        }
        //----------------------------------------------------------------------------\\
        /* This method takes in a scale factor, a quantity value,
        and a string representing the unit of measurement for that value
        and returns the correct unit of measurement based on the quantity entered*/
        public string CorrectQuantityAndMeasurement(double scale, double quantity,
                                               string unitOfMeasurement)
        {
            double scaleCorrectedQuantity = (quantity * scale); //The scale corrected quantity
            string correctQuantityAndMeasurement = scaleCorrectedQuantity + " "
                + unitOfMeasurement; //A string which stores the quantity and unit of measurement

            /*If the unit of measurement contains a '/' it means it is a custom unit as the '/'
            seperates the pular and singular forms */
            if (unitOfMeasurement.Contains('/'))
            {
                return IfCustomScale(unitOfMeasurement, correctQuantityAndMeasurement, scaleCorrectedQuantity);
            }

            else
            {
                //This statemetn calls the appropriate method based on the unit of measurement selected by the user
                switch (unitOfMeasurement)
                {
                    case "Teaspoon(s)":
                        return IfTeaspoons(scaleCorrectedQuantity);
                    case "Tablespoon(s)":
                        return IfTableSpoons(scaleCorrectedQuantity);
                    case "Cup(s)":
                        return IfCups(scaleCorrectedQuantity);
                    case "Kilograms(s)":
                        return IfKilograms(scaleCorrectedQuantity);
                    case "Gram(s)":
                        return IfGrams(scaleCorrectedQuantity);
                    default:
                        return null;
                }
            }
        }
        //----------------------------------------------------------------------------\\
        //This method will adjust the amounts and related scales if the scale set by the user was custom
        public string IfCustomScale(string unitOfMeasurement, string correctQuantityAndMeasurement,
                                    double scaleCorrectedQuantity)
        {
            // Split the unit of measurement string into two parts separated by '/'
            string[] parts = unitOfMeasurement.Split('/');

            // Construct a string with the scaled quantity and the appropriate
            // unit of measurement based on the parts array
            correctQuantityAndMeasurement = scaleCorrectedQuantity + " "
                + (scaleCorrectedQuantity == 1 ? parts[0] : parts[1]) + " Of";

            // Return the constructed string
            return correctQuantityAndMeasurement;
        }
        //----------------------------------------------------------------------------\\
        //This method will adjust the amounts and related scales if the scale set by the user was Teaspoons
        public string IfTeaspoons(double scaleCorrectedQuantity)
        {
            // If the quantity is a multiple of 4, convert to tablespoons and possibly cups
            if (scaleCorrectedQuantity % 4 == 0)
            {
                // Divide the quantity by 4 to convert to tablespoons
                scaleCorrectedQuantity /= 4.0;

                // Call the IfTableSpoons function to get the converted quantity and measurement
                string correctQuantityAndMeasurement = IfTableSpoons(scaleCorrectedQuantity);

                // If the converted quantity is greater than or equal to 16, convert to cups
                if (scaleCorrectedQuantity >= 16)
                {
                    // Divide the quantity by 16 to convert to cups and call the IfCups function
                    scaleCorrectedQuantity /= 16;
                    return IfCups(scaleCorrectedQuantity);
                }
                else
                {
                    // Return the converted quantity and measurement
                    return correctQuantityAndMeasurement;
                }
            }
            else
            {
                // If the quantity is not a multiple of 4, return the quantity
                // and teaspoon(s) as the unit of measurement
                string unitOfMeasurement = (scaleCorrectedQuantity > 1) ? "Teaspoons" : "Teaspoon";
                string correctQuantityAndMeasurement = scaleCorrectedQuantity
                       + " " + unitOfMeasurement + " Of";
                return correctQuantityAndMeasurement;
            }
        }
        //----------------------------------------------------------------------------\\
        //This method will adjust the amounts and related scales if the scale set by the user was Tablespoons
        public string IfTableSpoons(double scaleCorrectedQuantity)
        {
            // If the quantity is greater than or equal to 16, convert to cups
            if (scaleCorrectedQuantity >= 16)
            {
                // Divide the quantity by 16 to convert to cups and call the IfCups function
                scaleCorrectedQuantity /= 16;
                return IfCups(scaleCorrectedQuantity);
            }
            else if (scaleCorrectedQuantity < 1)
            {
                // If the quantity is less than 1, convert to teaspoons
                scaleCorrectedQuantity *= 4;
                return IfTeaspoons(scaleCorrectedQuantity);
            }
            else
            {
                // Otherwise, return the quantity and tablespoon(s) as the unit of measurement
                string unitOfMeasurement = (scaleCorrectedQuantity > 1) ? "Tablespoons" : "Tablespoon";
                string correctQuantityAndMeasurement = scaleCorrectedQuantity + " " + unitOfMeasurement + " Of";
                return correctQuantityAndMeasurement;
            }
        }
        //----------------------------------------------------------------------------\\
        //This method will adjust the amounts and related scales if the scale set by the user was cups
        public string IfCups(double scaleCorrectedQuantity)
        {
            // Return the quantity and cup(s) as the unit of measurement
            string unitOfMeasurement = (scaleCorrectedQuantity > 1) ? "Cups" : "Cup";
            string correctQuantityAndMeasurement = scaleCorrectedQuantity + " " + unitOfMeasurement + " Of";
            return correctQuantityAndMeasurement;
        }
        //----------------------------------------------------------------------------\\
        //This method will adjust the amounts and related scales if the scale set by the user was kilograms
        public string IfKilograms(double scaleCorrectedQuantity)
        {
            if (scaleCorrectedQuantity < 1)
            {
                scaleCorrectedQuantity *= 1000;
                return IfGrams(scaleCorrectedQuantity);
            }
            else
            {
                string unitOfMeasurement = (scaleCorrectedQuantity > 1) ? "Kilograms" : "Kilogram";
                string correctQuantityAndMeasurement = scaleCorrectedQuantity + " " + unitOfMeasurement + " Of";
                return correctQuantityAndMeasurement;
            }
        }
        //----------------------------------------------------------------------------\\
        //This method will adjust the amounts and related scales if the scale set by the user was grams
        public string IfGrams(double scaleCorrectedQuantity)
        {
            if (scaleCorrectedQuantity >= 1000)
            {
                scaleCorrectedQuantity /= 1000;
                return IfKilograms(scaleCorrectedQuantity);
            }
            else
            {
                string unitOfMeasurement = (scaleCorrectedQuantity > 1) ? "Grams" : "Gram";
                string correctQuantityAndMeasurement = scaleCorrectedQuantity + " " + unitOfMeasurement + " Of";
                return correctQuantityAndMeasurement;
            }
        }
    //----------------------------------------------------------------------------\\
        public double TotalCalories(double recipeScale)
        {
            double totalCalories = 0;

            foreach (Ingredient ingredient in IngredientsList)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories * recipeScale;
        }

        // This method provides calorie information based on the total calorie count of a meal.
        // It prints a string describing the energy content of the meal, categorized into four levels:
        // - Very low in energy content for meals with less than 75 calories.
        // - Relatively low in calories for meals between 75 and 150 calories.
        // - Slightly more substantial in energy content for meals between 150 and 300 calories.
        // - Significant in terms of energy content for meals with 300 or more calories.
        // The method also sets the console text color based on the calorie level for visual distinction.
        public string GetCalorieInformation(double totalCalories)
        {
            string low = " calories is considered very low in energy content, providing minimal nutrients for the body";
            string medium = " calories, is a relatively low amount of calories, offering only a modest amount of nutrients";
            string mediumHigh = " calories provides a slightly more substantial energy content, offering a bit more nutrients for the body";
            string high = " calories is considered significant in terms of energy content, providing a very substantial amount of nutrients for the body";

            if (totalCalories < 75) // If total calories is less than 75
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Set console text color to dark yellow
                return ("[" + totalCalories + low + "]"); // Return the low calorie information
            }
            else if (totalCalories >= 75 && totalCalories < 150) // If total calories is between 75 and 150
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Set console text color to yellow
                return ("[" + totalCalories + medium + "]");
            }
            else if (totalCalories >= 150 && totalCalories < 300) // If total calories is between 150 and 300
            {
                Console.ForegroundColor = ConsoleColor.Green; // Set console text color to green
                return ("[" + totalCalories + mediumHigh + "]");
            }
            else // For any other total calorie values
            {
                Animations animation = new Animations();
                AnimationsActions alert = new AnimationsActions(animation.CalorieAlert);
                alert();
                return ("[" + totalCalories + high + "]");
            }
        }
        //----------------------------------------------------------------------------\\
        // The information on the traditional food groups was provided by ChatGPT, an AI language model developed by OpenAI.
        // For more detailed information, please consult reliable nutrition sources or consult a healthcare professional.

        // The DisplayFoodGroupInfo method prints information about different food groups, ensuring line wrapping at 75 characters.
        // It splits the information into words, printing each word and adding a newline when necessary to maintain the line length.
        // After displaying the information, it prompts the user to press 'Enter' to return to the menu.
        public static void DisplayFoodGroupInfo()
        {
            // Define the information for each food group
            string fruits = "Fruits: Sweet and refreshing plant-based foods that provide essential vitamins, minerals, fiber, and antioxidants. Examples include apples, oranges, berries, and melons.";
            string vegetables = "Vegetables: Nourishing and diverse plant-based foods that offer a wide range of vitamins, minerals, fiber, and beneficial plant compounds. Examples include leafy greens, broccoli, carrots, and peppers.";
            string grains = "Grains: Staple foods made from cereal crops such as wheat, rice, oats, and corn. Grains are a significant source of carbohydrates, fiber, and various nutrients like B vitamins and minerals. Examples include bread, rice, pasta, and cereals.";
            string protein = "Protein: Foods that are rich in protein, necessary for building and repairing body tissues. This group includes animal sources like meat, poultry, fish, eggs, and dairy, as well as plant-based sources like legumes (beans, lentils), tofu, nuts, and seeds.";
            string dairy = "Dairy: Dairy products, such as milk, cheese, and yogurt, are excellent sources of calcium, protein, and other essential nutrients. Dairy provides bone-strengthening minerals and can be part of a healthy diet. Plant-based alternatives like soy or almond milk are also available for those who avoid dairy.";
            string fatsAndOils = "Fats or Oils: This group includes fats and oils that provide energy and essential fatty acids. While it's important to consume them in moderation, healthy fats from sources like avocados, nuts, seeds, and olive oil can be part of a balanced diet.";

            // Create an array to store the food group information
            string[] foodGroups = { fruits, vegetables, grains, protein, dairy, fatsAndOils };

            // Initialize variables
            string[] info;
            int limit = 0;

            Program.PrintTitle();
            Console.WriteLine("Here Is Information About The Food Groups Which Encompass All Ingredients:\n");

            // Iterate over each food group
            for (int group = 0; group < 6; group++)
            {
                // Split the information into words
                info = foodGroups[group].Split(' ', '-');

                // Iterate over each word in the food group information
                foreach (string word in info)
                {
                    // Check if adding the word exceeds the character limit
                    if (limit + word.Length > 75)
                    {
                        Console.WriteLine();
                        limit = 0;
                    }

                    Console.Write(word + ' ');
                    limit += word.Length + 1;
                }

                Console.WriteLine("\n");
            }

            Console.Write("\nPress 'Enter' To Return To Menu...");
            Console.ReadLine();
        }
    }

    //----------------------------------------------------------------------------\\
}


