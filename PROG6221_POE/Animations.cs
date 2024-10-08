﻿// russellschwedhelm
// ST10065470@vcconnect.edu.za
using System;
namespace PROG6221_POE
{
    public delegate void AnimationsActions();
    public class Animations
    {
        public void PrintMessage(string messageType, string message)
        {
            // if the messageType is "negative", print the message in red
            if (messageType.Equals("negative"))
            {
                Program.PrintTitle(); // call the PrintTitle method from the Program class
                Console.ForegroundColor = ConsoleColor.Red; // set the console text color to red
                Console.Write(message); // print the message
                LoadingAnimation(Console.ForegroundColor); // call the loadingAnimation method with the current console text color
                Console.ResetColor(); // reset the console text color to the default
            }
            // if the messageType is not "negative", print the message in green
            else
            {
                Program.PrintTitle(); // call the PrintTitle method from the Program class
                Console.ForegroundColor = ConsoleColor.Green; // set the console text color to green
                Console.Write(message); // print the message
                LoadingAnimation(Console.ForegroundColor); // call the loadingAnimation method with the current console text color
                Console.ResetColor(); // reset the console text color to the default
            }
        }
        //----------------------------------------------------------------------------\\
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
        // This methods prints out an alert animation
        public void CalorieAlert()
        {
            //Setting the colour of the font to be dark yellow
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            for (int loopNumber = 0; loopNumber < 4; loopNumber++)
            {
                for (int space = 4; space > loopNumber + 1; space--)
                {
                    Console.Write(" ");
                }
                for (int dot = 0; dot < loopNumber + 1; dot++)
                {
                    Console.Write(".");
                }
                Console.Write("ALERT");
                for (int dot = 0; dot < loopNumber + 1; dot++)
                {
                    Console.Write(".");
                }
                Thread.Sleep(430);
                ErrorControl.ClearCurrentConsoleLine();
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1); // Move the cursor to the beginning of the above line
        }
    }
}
//----------------------------------------------------------------------------\\