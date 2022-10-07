using System;
using System.Threading;

namespace HelloWorld
{
    class Program
    {
        static char[] charactersArray = new char[3556];
        static int[] trackPosition;
        static int colorSelection = 0;
        static string characters, isEndless = "N";

        static void Main(string[] args)
        {
            getUserInput();
            startExecution(characters, isEndless);
            Console.ReadKey();
        }

        /// <summary>
        /// It gets the user input and sets up the screen array
        /// </summary>
        static void getUserInput()
        {
            Console.WriteLine("Please input a string that is 1 to 127 character long:");
            characters = Console.ReadLine();

            setupScreenArray(characters);

            Console.WriteLine("Would you like to use endless mode? [Y/N]");
            Console.WriteLine("Is it set to 'No' by default.");
            isEndless = Console.ReadLine();
        }

        /// <summary>
        /// It starts the game
        /// </summary>
        /// <param name="characters">The characters that the user wants to use in the game.</param>
        /// <param name="response">The user's response to the question "Do you want to play in endless
        /// mode?"</param>
        static void startExecution(string characters, string response)
        {
            if (response == "N") startNotEndlessMode(characters);
            else startEndlessMode(characters);
        }

        /// <summary>
        /// It takes a string and creates an array of characters from that string.
        /// </summary>
        /// <param name="String">The string to be displayed on the screen.</param>
        static void setupScreenArray(String characters)
        {
            trackPosition = new int[characters.Length];

            for (int i = 0; i < charactersArray.Length; i++)
            {
                if (i < characters.Length)
                {
                    charactersArray[i] = characters[i];
                    trackPosition[i] = i;
                    continue;
                }

                charactersArray[i] = ' ';
            }
        }

        /// <summary>
        /// It takes a string of characters and displays them in a loop, moving them across the screen.
        /// </summary>
        /// <param name="String">characters</param>
        static void startEndlessMode(String characters)
        {
            int y = 0;
            Console.WriteLine(charactersArray);

            while (true)
            {

                for (int x = 0; x < y + 1; x++)
                {
                    int pos = trackPosition[x];
                    char current = charactersArray[pos];
                    int next = (trackPosition[x] + 127 >= 3556) ? trackPosition[x] %= 127 : trackPosition[x] += 127;

                    charactersArray[pos] = ' ';
                    charactersArray[next] = current;

                    Console.Clear();
                }

                displayTextSequence(charactersArray);
                delay();

                if (y < characters.Length - 1) y++;
            }
        }

        /// <summary>
        /// It takes a string of characters and displays them in a loop, one character at a time, until
        /// the last character has been displayed
        /// </summary>
        /// <param name="String">characters</param>
        static void startNotEndlessMode(String characters)
        {
            int y = 0;
            bool isEnded = false;
            Console.WriteLine(charactersArray);

            while (!isEnded)
            {

                for (int x = 0; x < y + 1; x++)
                {
                    int pos = trackPosition[x];
                    char current = charactersArray[pos];
                    int next = (trackPosition[x] + 127 >= 3556) ? trackPosition[x] = trackPosition[x] : trackPosition[x] += 127;

                    charactersArray[pos] = ' ';
                    charactersArray[next] = current;

                    if (x == characters.Length - 1 && pos == next) isEnded = true;

                    Console.Clear();
                }

                displayTextSequence(charactersArray);
                delay();

                if (y < characters.Length - 1) y++;
            }

            Console.Write("Animation Finished!");
        }

        /// <summary>
        /// It waits for 50 milliseconds.
        /// </summary>
        static void delay()
        {
            Thread.Sleep(50);
        }

        /// <summary>
        /// This function changes the color of the text in the console window.
        /// </summary>
        static void changeTextColor()
        {
            ConsoleColor[] arrayColor = { ConsoleColor.White, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.DarkYellow, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta };

            Console.ForegroundColor = arrayColor[colorSelection];
            if (colorSelection < arrayColor.Length - 1) colorSelection++;
            else colorSelection = 0;
        }

        /// <summary>
        /// It takes an array of characters, and displays them one by one, changing the color of the
        /// text every 127 characters.
        /// </summary>
        /// <param name="charactersArray">The array of characters that will be displayed.</param>
        static void displayTextSequence(char[] charactersArray)
        {
            for (int y = 0; y < 3556; y++) { 
                if(y % 127 == 0 && y != 3429) changeTextColor();
                if (y % 127 == 0 && y == 3429) Console.ForegroundColor = ConsoleColor.White;

                Console.Write(charactersArray[y]);
            }

            colorSelection = 0;
        }
    }
}