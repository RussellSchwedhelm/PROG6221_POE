# ST10065470
# Russell Schwedhelm
# PROG6221_POE
# Recipe Book Programme

This program is a console application that enables users to create, display, scale, and delete recipes using C# programming. The application provides a user-friendly interface that is easy to navigate, making it accessible to beginners and experienced programmers alike.

Two interesting features of this program are the use of animations when displaying messages or prompts in the console. This not only makes the program more engaging and enjoyable to use but also helps users stay informed about the program's progress. Additionally, this program has the ability to handle both words and numbers when quantities are concerned, allowing the user to enter, for example, "one" or 1 and expect the same reaction and output from the programme. 

The user can enter ingredients with different units of measurement, such as cups, tablespoons, teaspoons, grams, kilograms or custom units. The program will handle them accordingly so that the output is consistent and accurate. It does this by automatic scaling of non-custom units. Such as converting grams to kilograms or tablespoons to cups.

Additionally, error-handling features have been integrated into the program to prevent crashes or unexpected behaviour in case the user enters invalid input. For example, if the user enters a non-numeric value where a numeric value is expected, the program will display an error message and prompt the user to enter valid input.

## Getting Started

To run this program, you will need a C# development environment, such as Visual Studio, installed on your computer. If you don't have it installed, you can download it for free from the official Microsoft website:

https://visualstudio.microsoft.com/downloads/

After installing the development environment, follow these steps:

1. Open Visual Studio and select "Open a project or solution" from the start menu. Navigate to the project directory and select the .sln file to open the project.

2. Once the project is open in Visual Studio, select "Build" from the top menu bar, and then select "Build Solution". This will compile the code and create an executable file.

3. If the build is successful, you can run the program by pressing the "Start" button in the toolbar or by pressing the F5 key on your keyboard. This will launch the program in debug mode.

4. If you encounter any errors during the build or run process, check the output window for error messages. You can open the output window by selecting "View" from the top menu bar, then selecting "Output".

It's important to note that the program runs on .NET 7 framework. If you don't have it installed on your computer, you'll need to download and install it before you can run the program. You can download the .NET 7 framework from the official Microsoft website:

https://dotnet.microsoft.com/download/dotnet/7.0

Once you have installed .NET 7, you should be able to build and run the program without any issues.

## Usage

When you run the program, you will be presented with a menu of options that you can select from by entering a number corresponding to the option you wish to choose. The menu options are as follows:

1) Enter New Recipe
2) Display Recipe
3) Delete Recipe
4) Display Food Group Info
5) Exit

To select an option, simply enter the number that corresponds to it and press "Enter" on your keyboard.

If you select option 1, you will begin the process of entering a new recipe. The program will guide you through a series of prompts to input the necessary information to create a new recipe.

If you select option 2, you will begin the process of displaying a stored recipe. The program will guide you through a series of prompts to display a stored recipe.

If you select option 3, you will begin the process of deleting a stored recipe. The program will guide you through a series of prompts to delete a stored recipe.

If you select option 4, information will be displayed explaining what each of the food groups are and basic information about it.

If you select option 5, the program will exit.

## Creating New Recipe
If the user selects 1) from the main menu the prompts that will follow will guide the user through creating a new recipe. If there is a recipe stored, the user will be asked to delete that recipe or the operation will be aborted. If the user confirms by replying with "yes"/"y" the recipe will be deleted and the process of creating a new recipe will continue. If they respond with "no"/"n" then the operation will be aborted. None of the inputs can be null. None of the quantities can be a negative number. If 0 steps or ingredients are entered the operation will be aborted and the user will be informed why. The user can enter quantities in the form of a word or in the form of a numeric value ("1"/"one").

## Displaying A Recipe
If the user selects 2) from the main menu the prompts that will follow will guide the user through displaying an existing recipe. The programme will check if a recipe already exists in storage and if it does not the user will be informed of such and the operation will be aborted. If a recipe(s) do exist the user will be prompted to select which recipe must be displayed and then select the scale of the recipe's ingredients (1x, 2x, 3x, 0.5x). The user will have the option of resetting the scale after this.

## Deleting A Recipe
If the user selects 3) from the main menu the prompts that will follow will guide the user through deleting an existing recipe. The programme will check if a recipe exists in storage and if it does not the user will be informed of such and the operation will be aborted. If a recipe does exist in storage the user will be asked to confirm if they would like to delete the recipe. If the user confirms by replying with "yes"/"y" the recipe will be deleted. If they respond with "no"/"n" then the operation will be aborted. Any other input will be considered invalid and will ask the user to confirm if they would like to delete the recipe again.

## Getting Food Group Information
If the user selects 4) from the main menu, what follows will be a page with information about the 6 traditional food groups (Fruits, Vegetables, Grains, Protein, Dairy and Fats And Oils). This information will remain on screen until the user prompts the console to return to the main menu.

## Exiting The Programme
If the user selects 5) from the main menu the programme will terminate.

# Updates
## Program Updates
### Adding Steps And Ingredients
The process to add steps and ingredients has been changed. The user no longer has to indicate how many of either they would like to store before they are entered. Instead, they can continuously add both until they indicate to the console that they have added all nessesary steps.

### Information Storage
All user entered data is now stored in generic collections. Steps and ingredients are stored within lists and the recipes are stores in a sorted dictionary. The sorted dictionary automatically organises the recipes by name so that they are displayed in order when the user is at the recipe selection page.

### Ingredient Changes
Changes to the ingredient object include the addition of calories and food groups. The user indicates what these are during the creating of a new recipe. They are then used by the program elsewhere to inform the user about the overall state of a recipe.

### Calorie Alert
Different ranges of calories have different messages associated with them and these messages display useful information to the user. When a recipe is over 300 calories, an animation is played to alert the user to the infomration being displayed. This alert does not display through the other a calorie ranges. The alter is also implemented through the use of a delegate.

### Unit Test
The calorie alters and messages method was tested for several use cases. Negative test values were not included as these would be filtered out before they were stored and the method would never encounter them. Instead all message popups were tested for at the various calorie ranges.

### Menu
The menu's functionality was changed so that methods were called through the use of an array of delegates. A mroe detailed and interesting menu icon was also added and is displayed at the top of the program.

## References
### Methods
WordToDouble - ChatGPT. (2023, April 25). C# method to convert a word to a number (double) [Source code]. https://github.com/ChatGPT/Code-Samples/blob/main/CSharp/WordToDouble.cs

## GitHub Repository Link
https://github.com/VCCT-PROG6221-2023-Grp2/PROG6221_POE.git

## License
This project is licensed under the MIT License - see the LICENSE.txt file for details.
