using System.Diagnostics.Metrics;
using System.Drawing;

namespace PROG6221_POE
{
    public delegate void MenuAction();
    class Program
    {
        //A dictionary which stores created recipes
        SortedDictionary<string, Recipe> recipeList = new SortedDictionary<string, Recipe>(StringComparer.OrdinalIgnoreCase);
        ErrorControl errorControl = new ErrorControl();
        Animations animation = new Animations();

        private MenuAction[] menuActions;

        public Program()
        {
            InitializeMenuActions();
        }
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
        // The ASCII text art used in this code is obtained from the website:
        // https://patorjk.com/software/taag/#p=display&h=2&f=Doom&t=RECIPE%20BOOK
        public static void PrintTitle()
        {
            // Clear the console.
            Console.Clear();

            // Reset the console colours to their defaults.
            Console.ResetColor();

            // Print the programme title.
            Console.WriteLine("______ _____ _____ ___________ _____  ______ ___" +
                "__ _____ _   __\n| ___ \\  ___/  __ \\_   _| ___ \\  ___| | ___" +
                " \\  _  |  _  | | / /\n| |_/ / |__ | /  \\/ | | | |_/ / |__   |" +
                " |_/ / | | | | | | |/ / \n|    /|  __|| |     | | |  __/|  __|" +
                "  | ___ \\ | | | | | |    \\ \n| |\\ \\| |___| \\__/\\_| |_| | " +
                "  | |___  | |_/ | \\_/ | \\_/ / |\\  \\\n\\_| \\_\\____/ \\___" +
                "_/\\___/\\_|   \\____/  \\____/ \\___/ \\___/\\_| \\_/");

            // Print a line to separate the title.
            Console.WriteLine("---------------------------------------------------------------");
        }
        //----------------------------------------------------------------------------\\

        private void InitializeMenuActions()
        {
            menuActions = new MenuAction[]
            {
                CreateRecipe,
                DisplayRecipeAction,
                DeleteRecipe,
                Recipe.DisplayFoodGroupInfo,
                ExitProgram
            };
        }

        //----------------------------------------------------------------------------\\
        //This method displays a main menu and calls the appropriate method based on user selection
        public void Menu()
        {
            string userInput = ""; // Variable to store user input
            int menuSelection = 0; // Variable to store the selected menu option

            while (true)
            {
                do
                {
                    // Print the program title and menu options.
                    PrintTitle();
                    Console.WriteLine("1) Enter New Recipe");
                    Console.WriteLine("2) Display Recipe");
                    Console.WriteLine("3) Delete Recipe");
                    Console.WriteLine("4) Display Food Group Info");
                    Console.WriteLine("5) Exit");
                    Console.Write("\nEnter Your Numeric Selection: ");

                    userInput = Console.ReadLine(); // Read user input from the console

                } while (errorControl.CheckForPositiveNumber(userInput) == -1); // Repeat the loop until a valid numeric input is entered

                // Parsing the user's menu selection as an integer.
                menuSelection = int.Parse(userInput);

                if (menuSelection >= 1 && menuSelection <= menuActions.Length)
                {
                    // Get the selected action based on the menu selection
                    MenuAction selectedAction = menuActions[menuSelection - 1];
                    selectedAction(); // Execute the selected action
                }
                else
                {
                    errorControl.IncorrectEntryPrompt(); // Display an error prompt for an invalid menu selection
                }
            }
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
            string userInput; // Stores user input
            int convertedUserInput; // Stores the converted user input as an integer
            int checkedUserInput; // Stores the checked user input (1 for Yes, 2 for No, 0 for invalid input)
            int recipeNum = 1; // Counter for displaying recipe numbers
            string recipeName = ""; // Stores the name of the selected recipe

            // Check if there are any saved recipes
            if (recipeList.Count == 0)
            {
                // Display an error message if there are no saved recipes
                animation.PrintMessage("negative", "No Recipes Saved");
                return;
            }

            Console.Write("Select The Recipe You Would Like To Delete: \n");

            // Display the list of recipe numbers and names
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
                    // Retrieve the recipe name based on the user input (case-insensitive)
                    recipeName = recipeList.FirstOrDefault(pair => pair.Key.Equals(userInput, StringComparison.OrdinalIgnoreCase)).Key;
                }
                else if (userInput.ToLower().Equals("abort delete") || (recipeList.Count + 1 + "").Equals(userInput))
                {
                    return; // User chooses to abort the delete operation
                }
                else
                {
                    convertedUserInput = (int)errorControl.CheckForPositiveNumber(userInput);

                    if (convertedUserInput > 0 && convertedUserInput <= recipeNum)
                    {
                        // Retrieve the recipe name based on the converted user input
                        recipeName = recipeList.ElementAt(convertedUserInput - 1).Key;
                    }
                }
            } while (recipeName == ""); // Repeat if the input is invalid

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
                        recipeList.Remove(recipeName); // Remove the recipe from the recipe list
                        animation.PrintMessage("positive", "Recipe Deleted");
                        break;
                    default:
                        break;
                }

                // Repeat if incorrect input was entered
            } while (checkedUserInput == 0);
        }


        //----------------------------------------------------------------------------\\
        /*This method takes the user through the process of creating a new 
         * recipe, choosing its' unit of measurement and deleting any already existing recipes */
        public void CreateRecipe()
        {
            // Declaring variables
            string userInput; // Used to take user input from the console
            string recipeName; // Used to store the user inputted recipe name
            string continueAddition; // Used to store the user's decision as to continue the addition of steps or ingredients
            int numIngredients = 0; // Used to store the user inputted number of ingredients
            int numSteps = 0; // Used to store the user inputted number of steps

            // Declare and initialize variables for the ingredients
            string ingredientName;
            double ingredientQuantity = -1;
            string ingredientUnitOfMeasurement = "";
            string ingredientFoodGroup = "";
            int ingredientCalories = -1;

            // Declare a new recipe object
            Recipe newRecipe = new Recipe();

            // Prompt the user to enter the name of the new recipe and check if it's not null
            do
            {
                PrintTitle();
                Console.Write("Please Enter The Name Of The New Recipe: ");
                recipeName = Console.ReadLine();
            } while (errorControl.CheckForNull(recipeName) == false || errorControl.CheckForRecipe(recipeName, recipeList) == true);

            // Prompt the user to enter the number of ingredients and check if it's a number
            do
            {
                PrintTitle();
                Console.WriteLine("Recipe: " + recipeName);
                Console.WriteLine();
                do
                {
                    Console.Write("Would You Like To Add An Ingredient (Y/N): ");
                    continueAddition = Console.ReadLine();
                } while (errorControl.CheckYesOrNo(continueAddition) == 0);
                

                if (errorControl.CheckYesOrNo(continueAddition) == 1)
                {
                    numIngredients++;

                    // Prompt the user to enter the name of the ingredient until a non-null value is entered
                    do
                    {
                        PrintTitle();
                        Console.WriteLine("Recipe: " + recipeName);
                        Console.Write("\nPlease Enter The Name Of The Ingredient: ");
                        ingredientName = Console.ReadLine();
                    } while (errorControl.CheckForNull(ingredientName) == false);

                    Console.WriteLine();

                    // Prompt the user to select the unit of measurement for the ingredient
                    Console.Write("Please Select The Unit Of Measurement For \"" + ingredientName + "\": ");
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

                    // If the user chose option 6 above
                    if (ingredientUnitOfMeasurement == "Custom Unit")
                    {
                        ingredientUnitOfMeasurement = SetCustomScale();
                    }

                    Console.WriteLine();

                    // Prompt the user to enter the quantity for the ingredient until a valid number is entered
                    do
                    {
                        Console.Write("Please Enter The Quantity For \"" + ingredientName + "\": ");
                        userInput = Console.ReadLine().ToLower();

                        // Convert the string to a double after error checking to ensure that it is a numeric value >= 0
                        ingredientQuantity = errorControl.CheckForPositiveNumber(userInput);
                    } while (ingredientQuantity == -1);

                    Console.WriteLine();

                    // Prompt the user to enter the amount of calories in the ingredient until a valid number is entered
                    do
                    {
                        Console.Write("Please Enter The Amount Of Calories In \"" + ingredientName + "\": ");
                        userInput = Console.ReadLine();
                        ingredientCalories = (int)errorControl.CheckForPositiveNumber(userInput);
                    } while (ingredientCalories == -1);

                    Console.WriteLine();

                    // Prompt the user to select the food group of the ingredient
                    Console.Write("Please Select The Food Group Of \"" + ingredientName + "\": ");
                    Console.Write("\n1) Fruits" +
                                  "\n2) Vegetables" +
                                  "\n3) Grains" +
                                  "\n4) Protein" +
                                  "\n5) Dairy" +
                                  "\n6) Fats Or Oils\n\n");

                    do
                    {
                        Console.Write("Enter Your Selection: ");

                        userInput = Console.ReadLine();
                        ingredientFoodGroup = errorControl.CheckSelectFoodGroup(userInput);
                    } while (ingredientFoodGroup.Equals("Invalid"));

                    // Add the ingredient to the new recipe object
                    newRecipe.addIngredient(ingredientName, ingredientUnitOfMeasurement,
                                            ingredientQuantity, ingredientCalories, ingredientFoodGroup);
                }

            } while (errorControl.CheckYesOrNo(continueAddition) == 1);

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
                do
                {
                    Console.Write("Would You Like To Add A Step (Y/N): ");
                    continueAddition = Console.ReadLine();
                } while (errorControl.CheckYesOrNo(continueAddition) == 0);

                if (errorControl.CheckYesOrNo(continueAddition) == 1)
                {
                    string stepInfo; // Declare a string variable to hold the step information

                    numSteps++;

                    PrintTitle(); // Call the PrintTitle method to display the program title
                    Console.WriteLine("Recipe: " + recipeName); // Display the recipe name
                    Console.WriteLine(); // Add a blank line for spacing

                    // Prompt the user to enter step information until a non-null value is entered
                    do
                    {
                        Console.Write("Please Enter Step Number " + (numSteps) + ": "); // Display a message with the current step number
                        stepInfo = Console.ReadLine(); // Read the user input and store it in the stepInfo variable
                    } while (errorControl.CheckForNull(stepInfo) == false);

                    // Add the step to the new recipe object
                    newRecipe.addStep(stepInfo);
                }
            } while (errorControl.CheckYesOrNo(continueAddition) == 1);

            // If the number of steps is 0, print an error message and abort creating the recipe
            if (numSteps == 0)
            {
                animation.PrintMessage("negative", "No Steps To Be Added. Create Recipe Aborted");
                return;
            }

            // Add the recipe to the recipeList
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

            // Initialize variables for recipe selection and scale.
            int recipeNum = 1; // Counter for displaying recipe number
            double recipeScale = 0; // Scale factor of the selected recipe
            string userInput; // Variable for storing user's input
            string recipeName = ""; // String used to store the name of a selected recipe
            int convertedUserInput = -1; // Variable for storing user's input converted to an int
            double totalCalories = 0;
            Recipe recipeToDisplay = recipeList.GetValueOrDefault("");

            // Check if there are any recipes saved.
            if (recipeList.Count == 0)
            {
                // Display an error message and return to the main menu.
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

                // Check if the user's input matches a recipe name in a case-insensitive manner
                if (recipeList.Keys.Any(key => key.Equals(userInput, StringComparison.OrdinalIgnoreCase)))
                {
                    // Get the exact recipe name that matches the user's input
                    recipeName = recipeList.FirstOrDefault(pair => pair.Key.Equals(userInput, StringComparison.OrdinalIgnoreCase)).Key;
                }
                else if (userInput.ToLower().Equals("abort delete") || "6)".Contains(userInput))
                {
                    // Return to the main menu or abort deleting the recipe
                    return;
                }
                else
                {
                    // Convert the user's input to an integer and check if it falls within the range of available recipe numbers
                    convertedUserInput = (int)errorControl.CheckForPositiveNumber(userInput);
                    if (convertedUserInput > 0 && convertedUserInput <= recipeNum)
                    {
                        // Get the recipe name based on the user's input
                        recipeName = recipeList.ElementAt(convertedUserInput - 1).Key;
                    }
                }
            } while (errorControl.CheckForNull(recipeName) == false); // Check if the input is valid

            // Get the recipe object to display
            recipeToDisplay = recipeList.GetValueOrDefault(recipeName);

            RecipeActions getInfo = (input) =>
            {
                Console.Write(recipeToDisplay.GetCalorieInformation(input));
                Console.ResetColor();
                return null;
            };

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
                    userInput = Console.ReadLine().ToLower();

                    // Check if the input is valid and set the recipe scale
                    recipeScale = errorControl.CheckSetScale(userInput);
                } while (recipeScale == 0); // Continue the loop if the recipe scale is not set

                PrintTitle();
                Console.WriteLine("Recipe: " + recipeName + "\n");
                // Display the recipe with the selected scale
                Console.WriteLine(recipeToDisplay.DisplayRecipe(recipeScale));

                totalCalories = recipeToDisplay.TotalCalories(recipeScale);

                // Call the getInfo function to retrieve calorie information
                getInfo(totalCalories);
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
        // This method exits the program using the exit code 0
        public void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
//----------------------------------------------------------------------------\\