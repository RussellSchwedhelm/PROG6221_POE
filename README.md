# PROG6221_POE
# Recipe Book Programme

This programme is a console application that allows users to create, display, scale and delete recipes using C# programming. The programme also includes animations and error handling features. Furthermore the application allows the user to enter words or numbers, when quantities are concerned and they will be handled accordingly so that output is the same.

## Getting Started

To run this program, you will need a C# development environment such as Visual Studio. The program runs on .NET 7 framework.

Once you have opened the project in your development environment, you can build and run the program to execute the Main method of the Program class.

## Usage

When you run the program, you will be prompted to select a function from a menu of options:
1) Enter New Recipe
2) Display Recipe
3) Delete Recipe
4) Exit

If you select 1), you will begin the process of entering a new recipe

If you select 2), you will begin the process of displaying a stored recipe

If you select 3), you will begin the process of deleting a stored recipe

If you select 4), the program will exit.

The program also includes animations when displaying messages or prompts in the console. Additionally, error handling features are included to prevent crashes or unexpected behavior if invalid input is entered by the user.

## Creating New Recipe
If the user selects 1) from the main menu the prompts that will follow will guide the user through creating a new recipe. If there is a recipe stored, the user will be asked to delete that recipe or the operation will be aborted. If the user confirms by replying with "yes"/"y" the recipe will be deleted and the process of creating a new recipe will continue. If they respond with "no"/"n" then the operation will be aborted. None of the inputs can be null. None of the quantities can be a negtive number. If 0 steps or ingredients are entered the operation will be aborted and the user will be informed why. The user can enter quantities in the form of a word or in the form of a numeric value ("1"/"one").

## Displaying A Recipe
If the user selects 2) from the main menu the prompts that will follow will guide the user through displaying an existing recipe. The programme will check if a recipe already exists in storage and if it does not the user will be informed of such and the operation will be aborted. If a recipe(s) do exist the user will be prompted to select whcih recipe must be displayed and then select the scale of the recipe's ingredients (1x, 2x, 3x, 0.5x). The user will have the option of resetting the scale after this.

## Deleting A Recipe
If the user selects 3) from the main menu the prompts that will follow will guide the user through deleting an existing recipe. The programme will check if a recipe exists in storage and if it does not the user will be informed of such and the operation will be aborted. If a recipe does exist in storage the user will be asked to confirm if they would like to delete the recipe. If the user confirms by replying with "yes"/"y" the recipe will be deleted. If they respond with "no"/"n" then the operation will be aborted. Any other input will be considered as invalid and will ask the user to confirm if they would like to delete the recipe again.

## Exiting The Programme
If the user selects 4) from the main menu the programme will terminate.

## References
### Methods
WordToDouble - ChatGPT. (2023, April 25). C# method to convert a word to a number (double) [Source code]. https://github.com/ChatGPT/Code-Samples/blob/main/CSharp/WordToDouble.cs

## GitHub Repository Link
https://github.com/VCCT-PROG6221-2023-Grp2/PROG6221_POE.git

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.
