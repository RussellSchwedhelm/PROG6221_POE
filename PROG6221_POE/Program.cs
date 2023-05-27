using System.Drawing;

namespace PROG6221_POE
{
    class Program
    {
        //A dictionary which stores created recipes
        SortedDictionary<string, Recipe> recipeList = new SortedDictionary<string, Recipe>(StringComparer.OrdinalIgnoreCase);

        ErrorControl errorControl = new ErrorControl();
        Animations animation = new Animations();
        //----------------------------------------------------------------------------\\
        static void Main(string[] args)
        {
            // Create an instance of the Program class named "run" to avoid using all static methods.
            Program run = new Program();
            // Call the Menu method of the run instance.
            run.Menu();
        }

        //----------------------------------------------------------------------------\\
        /* This method clears the console, resets the console colours to their defaults,
        and prints the programme title and a separator. */
        public void PrintTitle()
        {
            // Clear the console.
            Console.Clear();

            // Reset the console colours to their defaults.
            Console.ResetColor();

            // Print the programme title.
            Console.WriteLine("RECIPE BOOK PROGRAMME");

            // Print a line to separate the title.
            Console.WriteLine("---------------------");
        }


        //----------------------------------------------------------------------------\\
        //This method displays a main menu and calls the appropriate method based on user selection
        public void Menu()
        {
            int menuSelection = 0;

            // Print the programme title and menu options.
            PrintTitle();
            Console.WriteLine("1) Enter New Recipe" +
                              "\n2) Display Recipe" +
                              "\n3) Delete Recipe" +
                              "\n4) Exit");
            Console.Write("\nEnter Your Numeric Selection: ");

            // Attempt to parse the user's menu selection as an integer.
            try
            {
                menuSelection = int.Parse(Console.ReadLine());
            }
            catch
            {
            }

            // Execute the appropriate action based on the user's selection.
            switch (menuSelection)
            {
                case 1:
                    // If the user selects option 1, create a new recipe.
                    PrintTitle();
                    CreateRecipe();
                    break;
                case 2:
                    // If the user selects option 2, display a recipe.
                    DisplayRecipeAction();
                    break;
                case 3:
                    // If the user selects option 3, delete a recipe.
                    DeleteRecipe();
                    break;
                case 4:
                    // If the user selects option 4, exit the programme.
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    // If the user enters an invalid selection, prompt them to try again.
                    errorControl.IncorrectEntryPrompt();
                    Console.Clear();
                    Menu();
                    break;
            }

            // After the selected action is executed, the menu displays again.
            Menu();
        }

        //----------------------------------------------------------------------------\\
        /*
        Deletes a recipe from the list of saved recipes, after prompting the user for confirmation.
        If there are no saved recipes to delete, an error message is displayed and the method exits.
        */
        public void DeleteRecipe()
        {
            // Print the title of the recipe book
            PrintTitle();

            // Initialize variables for user input
            string userInput;
            int convertedUserInput;
            int checkedUserInput;
            int recipeNum = 1;
            Recipe recipeToDisplay = null;
            string recipeName = "";

            // Check if there are any saved recipes
            if (recipeList.Count == 0)
            {
                // Display an error message if there are no saved recipes
                animation.PrintMessage("negative", "No Recipes Saved");
                return;
            }

            Console.Write("Select The Recipe You Would Like To Delete: \n");
            foreach (string key in recipeList.Keys)
            {
                Console.WriteLine(recipeNum + ") " + key); // Display the recipe number and name
                recipeNum++; // Increment the recipe number counter
            }
            Console.Write(recipeNum + ") Abort Delete\n\n");

            do
            {
                Console.Write("Enter Your Selection: ");
                userInput = Console.ReadLine(); // Read user's input from the console
                if (recipeList.Keys.Any(key => key.Equals(userInput, StringComparison.OrdinalIgnoreCase)))
                {
                    recipeName = recipeList.FirstOrDefault(pair => pair.Key.Equals(userInput, StringComparison.OrdinalIgnoreCase)).Key;
                }
                else if (userInput.ToLower().Equals("abort delete") || "6)".Contains(userInput))
                {
                    return;
                }
                else
                {
                    convertedUserInput = (int)errorControl.CheckForPositiveNumber(userInput);
                    if (convertedUserInput > 0 && convertedUserInput <= recipeNum)
                    {
                        recipeName = recipeList.ElementAt(convertedUserInput).Key;
                    }
                }
            } while (recipeName == ""); // Check if the input is valid

            do
            {
                // Print the title of the recipe book
                PrintTitle();

                // Prompt the user for confirmation to delete the saved recipe
                Console.Write("Are You Sure You Want To Delete \"" + recipeName + "\" (Y/N): ");
                userInput = Console.ReadLine().ToLower();

                checkedUserInput = errorControl.CheckYesOrNo(userInput);

                switch (checkedUserInput)
                {
                    case 1:
                        recipeList.Remove(recipeName);
                        animation.PrintMessage("positive", "Recipe Deleted");
                        break;
                    default:
                        break;
                }
                //Repeat if incorrect input was entered
            } while (checkedUserInput == 0);
        }

        //----------------------------------------------------------------------------\\
        /*This method takes the user through the process of creating a new 
         * recipe, choosing its' unit of measurement and deleting any already existing recipes */
        public void CreateRecipe()
        {
            //Declaring variables
            string userInput; //Used to take user input from the console
            string recipeName; //Used to store the user inputted recipe name
            int numIngredients = -1; //Used to store the user inputted number of ingredients
            int numSteps = -1; //Used to store the user inputted number of steps

            // Declare a new recipe object
            Recipe newRecipe;

            // Prompt the user to enter the name of the new recipe and check if it's not null
            do
            {
                PrintTitle();
                Console.Write("Please Enter The Name Of The New Recipe: ");
                recipeName = Console.ReadLine();
            } while (errorControl.CheckForNull(recipeName) == false && errorControl.CheckForRecipe(recipeName, recipeList) == false);

            // Prompt the user to enter the number of ingredients and check if it's a number
            do
            {
                PrintTitle();
                Console.WriteLine("Recipe: " + recipeName);
                Console.WriteLine();
                Console.Write("How Many Ingredients Do You Want To Add: ");
                userInput = Console.ReadLine().ToLower();

                // Convert the user input to an integer
                numIngredients = (int)errorControl.CheckForPositiveNumber(userInput);
            } while (numIngredients == -1);

            // If the number of ingredients is 0, print an error message and abort creating the recipe
            if (numIngredients == 0)
            {
                animation.PrintMessage("negative", "No Ingredients To Be Added. Create Recipe Aborted");
                return;
            }

            // Prompt the user to enter the number of steps and check if it's a number
            do
            {
                PrintTitle();
                Console.WriteLine("Recipe: " + recipeName);
                Console.WriteLine();
                Console.Write("How Many Steps Do You Want To Add: ");
                userInput = Console.ReadLine().ToLower();

                // Convert the user input to an integer
                numSteps = (int)errorControl.CheckForPositiveNumber(userInput);
            } while (numSteps == -1);

            // If the number of steps is 0, print an error message and abort creating the recipe
            if (numSteps == 0)
            {
                animation.PrintMessage("negative", "No Steps To Be Added. Create Recipe Aborted");
                return;
            }

            // Create a new recipe object with the given name, number of ingredients and steps
            newRecipe = new Recipe();

            // Declare and initialize the variables for the ingredients
            string ingredientName;
            double ingredientQuantity = -1;
            string ingredientUnitOfMeasurement = "";
            string ingredientFoodGroup = "";
            int ingredientCalories = -1;

            // Loop through the ingredients and prompt the user to enter their name, unit of measurement and quantity
            for (int ingredient = 0; ingredient < numIngredients; ingredient++)
            {
                // Do this whule the user input is null
                do
                {
                    PrintTitle();
                    Console.WriteLine("Recipe: " + recipeName);
                    Console.Write("\nPlease Enter The Name Of The Ingrediant: ");
                    ingredientName = Console.ReadLine();
                } while (errorControl.CheckForNull(ingredientName) == false);

                Console.WriteLine();


                Console.Write("Please Select The Unit Of Measurement For \""
                          + ingredientName + "\": ");
                Console.WriteLine("\n1) Teaspoon(s)" +
                                  "\n2) Tablespoon(s)" +
                                  "\n3) Cup(s)" +
                                  "\n4) Gram(s)" +
                                  "\n5) Kilogram(s)" +
                                  "\n6) Custom Unit" +
                                  "\n");

                // Do this while the user input does not fall within the range 1 - 6
                do
                {
                    Console.Write("Enter Your Selection: ");
                    userInput = Console.ReadLine().ToLower();

                    ingredientUnitOfMeasurement = errorControl.CheckSelectUnit(userInput);

                } while (ingredientUnitOfMeasurement.Equals("Invalid"));

                //If the user chose option 6 above
                if (ingredientUnitOfMeasurement == "Custom Unit")
                {
                    ingredientUnitOfMeasurement = SetCustomScale();
                }

                Console.WriteLine();

                //Do this while the user input is not a valid number. ie. < 0
                do
                {
                    Console.Write("Please Enter The Quantity For \""
                                  + ingredientName + "\": ");
                    userInput = Console.ReadLine().ToLower();

                    //Converting the stirng to a double after error checking to ensure that it is a numeric value >= 0
                    ingredientQuantity = errorControl.CheckForPositiveNumber(userInput);
                } while (ingredientQuantity == -1);

                Console.WriteLine();

                do
                {
                    Console.Write("Please Enter The Amount Of Calories In \""
                                  + ingredientName + "\": ");
                    userInput = Console.ReadLine();
                    ingredientCalories = (int) errorControl.CheckForPositiveNumber(userInput);
                } while (ingredientCalories == -1);

                Console.WriteLine();

                do
                {
                    Console.Write("Please Select The Food Group Of \"" + ingredientName + "\": ");
                    Console.Write("\n1) Fruits" +
                                  "\n2) Vegitables" +
                                  "\n3) Grains" +
                                  "\n4) Protein" +
                                  "\n5) Dairy" +
                                  "\n6) Fats Or Oils");
                    Console.Write("\n\nEnter Your Selection: ");

                    userInput = Console.ReadLine();
                    ingredientFoodGroup = errorControl.CheckSelectFoodGroup(userInput);
                } while (ingredientFoodGroup.Equals("Invalid"));

                newRecipe.addIngredient(ingredientName, ingredientUnitOfMeasurement,
                                        ingredientQuantity, ingredientCalories, ingredientFoodGroup);

            }

            for (int step = 0; step < numSteps; step++) // Loop from step 0 to step (numSteps - 1)
            {
                string stepInfo; // Declare a string variable to hold the step information

                PrintTitle(); // Call the PrintTitle method to display the program title
                Console.WriteLine("Recipe: " + recipeName); // Display the recipe name
                Console.WriteLine(); // Add a blank line for spacing

                // Prompt the user to enter step information until a non-null value is entered
                do
                {
                    Console.Write("Please Enter Step Number " + (step + 1) + ": "); // Display a message with the current step number
                    stepInfo = Console.ReadLine(); // Read the user input and store it in the stepInfo variable
                } while (errorControl.CheckForNull(stepInfo) == false);

                newRecipe.addStep(stepInfo); // Call the addStep method of the newRecipe object to add the step to the recipe
            }

            //Add the recipe to the recipeList
            recipeList.Add(recipeName, newRecipe);
            animation.PrintMessage("positive", "Recipe Saved");

        }

        //----------------------------------------------------------------------------\\
        /* This method displays a list of saved recipes, prompts the user to 
         * select one and a scale, and displays the selected recipe with the chosen scale */
        public void DisplayRecipeAction()
        {
            // Print title of the application.
            PrintTitle();

            // Initialise variables for recipe selection and scale.
            int recipeNum = 1; // Counter for displaying recipe number
            int recipeIndex = 0; // Index of the selected recipe in the recipeList
            double recipeScale = 0; // Scale factor of the selected recipe
            string userInput; // Variable for storing user's input
            string recipeName = ""; // String used to store the name of a selected recipe
            int convertedUserInput = -1; // Variable for storing user's input converted to an int
            Recipe recipeToDisplay = recipeList.GetValueOrDefault("");

            // Check if there are any recipes saved.
            if (recipeList.Count == 0)
            {
                // Display an error message and return to the.
                animation.PrintMessage("negative", "No Recipes Saved");
                return;
            }

            // Prompt the user to select a recipe.
            Console.WriteLine("Please Select The Recipe You Want To Display:");

            // Display the list of recipes.

            foreach (string key in recipeList.Keys)
            {
                Console.WriteLine(recipeNum + ") " + key); // Display the recipe number and name
                recipeNum++; // Increment the recipe number counter
            }

            Console.WriteLine();

            // Get the user's recipe selection.
            do
            {
                Console.Write("Enter Your Selection: ");
                userInput = Console.ReadLine(); // Read user's input from the console
                if (recipeList.Keys.Any(key => key.Equals(userInput, StringComparison.OrdinalIgnoreCase)))
                {
                    recipeName = recipeList.FirstOrDefault(pair => pair.Key.Equals(userInput, StringComparison.OrdinalIgnoreCase)).Key;
                }

                else if (userInput.ToLower().Equals("abort delete") || "6)".Contains(userInput))
                {
                    return;
                }
                else
                {
                    convertedUserInput = (int)errorControl.CheckForPositiveNumber(userInput);
                    if (convertedUserInput > 0 && convertedUserInput <= recipeNum)
                    {
                        recipeName = recipeList.ElementAt(convertedUserInput).Key;
                    }
                }
            } while (errorControl.CheckForNull(recipeName) == false); // Check if the input is valid

            recipeToDisplay = recipeList.GetValueOrDefault(recipeName);

            // Loop for setting the scale of the selected recipe
            do
            {
                PrintTitle();
                Console.WriteLine("Recipe: " + recipeName + "\n");
                Console.WriteLine("Select Your Scale:" +
                                  "\n1) Default (1x)" +
                                  "\n2) Double (2x)" +
                                  "\n3) Triple (3x)" +
                                  "\n4) Half (0.5)" +
                                  "\n");

                // Loop for getting the user's scale selection
                do
                {
                    // Prompt the user to enter their selection of scale
                    Console.Write("Enter Your Selection Of Scale: ");
                    // Read the user input from the console, convert it to lowercase, and trim it to 2 characters
                    userInput = Console.ReadLine().ToLower();

                    recipeScale = errorControl.CheckSetScale(userInput); // Check if the input is valid and set the recipe scale
                } while (recipeScale == 0); // Continue the loop if the recipe scale is not set

                PrintTitle();
                Console.WriteLine("Recipe: " + recipeName+ "\n");
                Console.WriteLine(recipeToDisplay.DisplayRecipe(recipeScale)); // Display the recipe with the selected scale

                if (recipeToDisplay.TotalCalories(recipeScale) > 300)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow; // set the console text color to dark yellow
                    Console.Write("Caution! The Total Calories For This Recipe Are Over 300");
                    Console.ResetColor(); // reset the console text color to the default
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green; // set the console text color to green
                    Console.Write("The Total Calories For This Recipe Are Under Or Equal To 300");
                    Console.ResetColor(); // reset the console text color to the default
                }

                Console.WriteLine();

                // Prompt the user for the next action
                Console.WriteLine("\nWould You Like To:" +
                      "\n1) Reset Scale" +
                      "\n2) Return To Menu" +
                      "\n");
                Console.Write("Enter Your Numeric Selection: ");
                userInput = Console.ReadLine(); // Read user's input from the console
            } while (errorControl.CheckSetScaleMenuChoice(userInput)); // Continue the loop based on the user's selection
        }

        //----------------------------------------------------------------------------\\
        /* This method prompts the user to enter the singular and plural forms of a 
         * custom unit of measurement and returns them as a concatenated string. */
        public string SetCustomScale()
        {
            // Declare a string variable to hold the unit of measurement.
            string unitOfMeasurement;

            // Prompt the user to enter the singular form of the unit of measurement and read the input.
            Console.Write("\nPlease Enter The Singular Form Of Your Unit Of Measurement: ");
            unitOfMeasurement = Console.ReadLine();

            Console.WriteLine();

            // Prompt the user to enter the plural form of the unit of measurement and append it to the singular form.
            Console.Write("Please Enter The Plural Form Of Your Unit Of Measurement: ");
            unitOfMeasurement += "/" + Console.ReadLine();

            // Return the custom unit of measurement.
            return unitOfMeasurement;

        }
        //----------------------------------------------------------------------------\\
    }
}
//----------------------------------------------------------------------------\\