// russellschwedhelm
// ST10065470@vcconnect.edu.za
using System;
namespace PROG6221_POE
{
    public class Animations
    {
        public void PrintMessage(string messageType, string message)
        {
            // create a new instance of the Program class
            Program program = new Program();

            // if the messageType is "negative", print the message in red
            if (messageType.Equals("negative"))
            {
                program.PrintTitle(); // call the PrintTitle method from the Program class
                Console.ForegroundColor = ConsoleColor.Red; // set the console text color to red
                Console.Write(message); // print the message
                loadingAnimation(Console.ForegroundColor); // call the loadingAnimation method with the current console text color
                Console.ResetColor(); // reset the console text color to the default
            }
            // if the messageType is not "negative", print the message in green
            else
            {
                program.PrintTitle(); // call the PrintTitle method from the Program class
                Console.ForegroundColor = ConsoleColor.Green; // set the console text color to green
                Console.Write(message); // print the message
                loadingAnimation(Console.ForegroundColor); // call the loadingAnimation method with the current console text color
                Console.ResetColor(); // reset the console text color to the default
            }
        }
        //----------------------------------------------------------------------------\\
        //A method which prints a loadign animation when called
        public void loadingAnimation(ConsoleColor colour)
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
    }
        //----------------------------------------------------------------------------\\
}
//----------------------------------------------------------------------------\\