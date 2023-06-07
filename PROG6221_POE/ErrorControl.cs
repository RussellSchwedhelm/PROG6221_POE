using System;

namespace PROG6221_POE
{
    public class ErrorControl
    {
        //Displays an error message to the user prompting them to try again after entering invalid input.
        public void IncorrectEntryPrompt()
        {
            // Set text text colour to red
            Console.ForegroundColor = ConsoleColor.Red;
            // Clear the current console line
            ClearCurrentConsoleLine();
            Console.Write("Invalid Entry! Press Try Again");// Display the error message to the user
                                                            // Show a loading animation using the red text colour
            LoadingAnimation(Console.ForegroundColor);
            // Clear the current console line
            ClearCurrentConsoleLine();
            // Reset the console text colour to default
            Console.ResetColor();
            Console.WriteLine();
        }
        //----------------------------------------------------------------------------\\
        //Clears the current console line by printing blank spaces over the existing text.
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop - 1; // Get the current line position and subtract 1 to move one line above
            Console.SetCursorPosition(0, currentLineCursor); // Move the cursor to the beginning of the line
            Console.Write(new string(' ', Console.WindowWidth)); // Overwrite the line with spaces, clearing its content
            Console.SetCursorPosition(0, currentLineCursor); // Reset the cursor position back to the original line
        }


        //----------------------------------------------------------------------------\\
        //Prints a loading animation consisting of three dots, with a specified color.
        //A method which prints a loadign animation when called
        public void LoadingAnimation(ConsoleColor colour)
        {
            //Setting the colour of the text
            Console.ForegroundColor = colour;
            //A for loop which runs 3 times to print 3 '.' characters
            for (int pos = 0; pos < 3; pos++)
            {
                Thread.Sleep(400);//Making the thread pause for 400 milliseconds, creating the animation effect
                Console.Write(".");
            }
            Thread.Sleep(400);
        }
        //----------------------------------------------------------------------------\\
        // Checks if the user's answer contains "y" or "n" and returns 1 if "y", 2 if "n",
        // or 0 if neither. If it is neither, it calls the IncorrectEntryPrompt() method.
        public int CheckYesOrNo(string answer)
        {
            if (answer.ToLower().Contains("y")) // if answer contains "y"
            {
                return 1; // return 1
            }
            else if (answer.ToLower().Contains("n")) // else if answer contains "n"
            {
                return 2; // return 2
            }
            else // else
            {
                IncorrectEntryPrompt(); // call IncorrectEntryPrompt method
                return 0; // return 0
            }
        }
        //----------------------------------------------------------------------------\\
        // Checks if the user's input is null and returns false if it is,
        // or true if it is not. If it is null, it calls the IncorrectEntryPrompt() method.
        public bool CheckForNull(string userInput)
        {
            if (userInput.Equals("")) // if user input is empty
            {
                IncorrectEntryPrompt(); // call IncorrectEntryPrompt method
                return false; // return false
            }
            else // else
            {
                return true; // return true
            }
        }
        //----------------------------------------------------------------------------\\
        /* 
         * Check if the user's input Checks if the user's input is a positive number
         * and returns the input as a double if it is. If the input is less than 0 or
         * cannot be parsed as a double, it tries to convert it to a double using the
         * WordToDouble method and returns -1 to indicate failure. a positive number
        */
        public double CheckForPositiveNumber(string userInput)
        {
            try
            {
                // try to parse user input as a double and check if the number is greater than or equal to 0
                double input = double.Parse(userInput);
                if (input >= 0)
                {
                    return input; // return the input if successful
                }
                else
                {
                    throw new Exception(); // throw an exception if the input is less than 0
                }
            }
            catch
            {
                // if parsing as a double fails, try to convert the input to a double using the WordToDouble method
                double wordValue = WordToDouble(userInput);
                if (wordValue >= 0)
                {
                    return wordValue; // return the word value if successful
                }
                else
                {
                    return -1; // return -1 to indicate failure
                }
            }
        }
        //----------------------------------------------------------------------------\\
        // Checks if the user's input is a valid unit of measurement and returns the
        // corresponding unit of measurement as a string. If the input is not valid,
        // it returns an empty string.
        public string CheckSelectUnit(string userInput)
        {
            switch (userInput)
            {
                // If userInput is "1", "teaspoon", or "teaspoon(s)", return "Teaspoon(s)"
                case "1":
                case "teaspoon":
                case "teaspoons":
                    return "Teaspoon(s)";

                // If userInput is "2", "tablespoon", or "tablespoon(s)", return "Tablespoon(s)"
                case "2":
                case "tablespoon":
                case "tablespoons":
                    return "Tablespoon(s)";

                // If userInput is "3", "cup", or "cup(s)", return "Cup(s)"
                case "3":
                case "cup":
                case "cups":
                    return "Cup(s)";

                // If userInput is "4", "gram", or "gram(s)", return "Gram(s)"
                case "4":
                case "gram":
                case "grams":
                    return "Gram(s)";

                // If userInput is "5", "kilogram", or "kilogram(s)", return "Kilogram(s)"
                case "5":
                case "kilogram":
                case "kilograms":
                    return "Kilogram(s)";

                // If userInput is "6", "custom unit", or "custom", return "Custom Unit"
                case "6":
                case "custom unit":
                case "custom":
                    return "Custom Unit";

                // If userInput is none of the above, call IncorrectEntryPrompt method and return "Invalid"
                default:
                    IncorrectEntryPrompt();
                    return "Invalid";
            }
        }
        //----------------------------------------------------------------------------\\
        //The code takes a string input and returns a double value based on the input
        //matching specific cases or a default value of 0 if none of the cases match.
        public double CheckSetScale(string userInput)
        {
            switch (userInput) 
            { 
                // If the user input contains '1' or is equal to 'default'
                case "1":
                case "default":
                    return 1;
                // If the user input contains '2' or is equal to 'double'
                case "2":
                case "double":
                    return 2;
                // If the user input contains '3' or is equal to 'triple'
                case "3":
                case "triple":
                    return 3;
                // If the user input contains '4', is equal to 'half', or is equal to '0.5'
                case "4":
                case "half":
                case "0.5":
                    return 0.5;
                default:
                    // Prompt the user that their entry is incorrect
                    IncorrectEntryPrompt();
                    // Return 0 as a default value
                    return 0;
            }
        }
        //----------------------------------------------------------------------------\\
        /* This function checks if the user input is either "1) reset scale" or "2) Return To Menu",
         * returning true or false respectively, and prompts the user for incorrect input, <summary>
         * This function checks if the user input is either "1) reset scale" or "2) Return To Menu",
         * always returning true as a default value.
        */
        public bool CheckSetScaleMenuChoice(string userInput)
        {
            // Check if the user input is '1) reset scale'
            if ("1) reset scale".Contains(userInput))
            {
                return true;
            }
            // Check if the user input is '2) Return To Menu'
            else if ("2) Return To Menu".Contains(userInput))
            {
                return false;
            }
            else
            {
                // Prompt the user that their entry is incorrect
                IncorrectEntryPrompt();
                // Return true as a default value
                return true;
            }
        }
        //----------------------------------------------------------------------------\\
        //This function parses an input recipe number, checks if it is less than or equal
        //to the number of available recipes, and returns a boolean value accordingly.
        public bool CheckForRecipe(string recipeName, SortedDictionary<string, Recipe> recipeList)
        {
            if (recipeList.Count == 0)
            {
                return false;
            }
            else
            {
                string[] keyList = recipeList.Keys.ToArray();
                if (keyList.Contains(recipeName))
                {
                    Animations animaiton = new Animations();
                    animaiton.PrintMessage("negative", "Recipe Already Exists");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Title: C# method to convert a word to a number (double)
        //Author: ChatGPT
        //Date: 2023-04-25

        // Source: https://github.com/openai/gpt-3-examples/blob/main/csharp/word-to-double-converter.cs
        //Availability: https://github.com/ChatGPT/Code-Samples/blob/main/CSharp/WordToDouble.cs

        // OpenAI is a research organization that aims to ensure that artificial intelligence (AI) benefits humanity. 
        // They are known for their work in developing natural language processing (NLP) and AI models, including GPT-3. 
        // For more information, visit their website at https://openai.com/.

        // Russell Schwedhelm is the author of this adapted code. This version includes modifications to fit specific use cases.

        //This method converts an enetered word to a double
        double WordToDouble(string userInput)
        {
            userInput = userInput.ToLower();
            // Define a dictionary of number words and their corresponding numeric values
            Dictionary<string, double> numberWords = new Dictionary<string, double>()
            {
                {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4}, {"five", 5}, {"six", 6},
                {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10}, {"eleven", 11}, {"twelve", 12},
                {"thirteen", 13}, {"fourteen", 14}, {"fifteen", 15}, {"sixteen", 16}, {"seventeen", 17},
                {"eighteen", 18}, {"nineteen", 19}, {"twenty", 20}, {"thirty", 30}, {"forty", 40}, {"fifty", 50},
                {"sixty", 60}, {"seventy", 70}, {"eighty", 80}, {"ninety", 90}, {"hundred", 100}, {"thousand", 1000},
                {"million", 1000000}, {"billion", 1000000000},
            };

            double totalValue = 0;
            double chunkValue = 0;
            bool hasHundred = false;

            string[] words = userInput.Split(' ', '-');
            foreach (string w in words)
            {
                if (numberWords.TryGetValue(w, out double value))
                {
                    if (value >= 100)
                    {
                        if (hasHundred)
                        {
                            // Invalid Input Prompt
                            IncorrectEntryPrompt();
                            return 0;
                        }
                        chunkValue *= value;
                        hasHundred = true;
                    }
                    else
                    {
                        chunkValue += value;
                    }
                }
                else if (w.Equals("and"))
                {
                    // do nothing
                }
                else
                {
                    // Invalid Input Prompt
                    IncorrectEntryPrompt();
                    return -1;
                }
            }

            if (chunkValue == 0 && hasHundred)
            {
                chunkValue = 100;
            }

            totalValue += chunkValue;

            return totalValue;
        }
        //----------------------------------------------------------------------------\\
        // Checks if the user's input is a valid food group selection and returns the
        // corresponding food group as a string. If the input is not valid, it returns an empty string.
        public string CheckSelectFoodGroup(string userInput)
        {
            switch (userInput.ToLower())
            {
                // If userInput is "1" or "fruits", return "Fruits"
                case "1":
                case "fruits":
                    return "Fruits";

                // If userInput is "2" or "vegetables", return "Vegetables"
                case "2":
                case "vegetables":
                    return "Vegetables";

                // If userInput is "3" or "grains", return "Grains"
                case "3":
                case "grains":
                    return "Grains";

                // If userInput is "4" or "protein", return "Protein"
                case "4":
                case "protein":
                    return "Protein";

                // If userInput is "5" or "dairy", return "Dairy"
                case "5":
                case "dairy":
                    return "Dairy";

                // If userInput is "6" or "fats or oils", return "Fats Or Oils"
                case "6":
                case "fats or oils":
                    return "Fats Or Oils";

                // If userInput is none of the above, call IncorrectEntryPrompt method and return "Invalid"
                default:
                    IncorrectEntryPrompt();
                    return "Invalid";
            }
        }

    }
}
//----------------------------------------------------------------------------\\