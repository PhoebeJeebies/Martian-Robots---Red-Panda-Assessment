using System.Text.RegularExpressions;
using static Martian_Robots.Utilities;

namespace Martian_Robots
{
    internal class Helpers
    {

        #region Parsing Methods

        public static Coordinate ParseCoordinate(string input)
        {
            string[] coordinates = input.Split(' ');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            // Validate the coordinates are within the expected range
            if (x < 0 || x > 50 || y < 0 || y > 50)
            {
                throw new FormatException("Invalid coordinate format. Expected two integers between 0 and 50.");
            }

            return new Coordinate(x, y);
        }

        #endregion

        #region  Input Methods
        public static string? RequestUserInput_SingleLine(string messageCode)
        {
            WriteMessage(Messages[messageCode]);
            return Console.ReadLine();
        }

        #endregion

        internal static string WrappedTextFormatter(string message)
        {
            return Regex.Replace(message, @"(.{1," + Console.WindowWidth + @"})(?:\s+|$)", "$1\n");
        }

        #region Output Methods

        public static void WriteMessage(string message, ConsoleColor? color = null)
        {
            Console.ForegroundColor = color ?? ConsoleColor.Yellow;
            Console.WriteLine(WrappedTextFormatter(message));
            Console.ResetColor();
        }
        public static void WriteOutput(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(WrappedTextFormatter(message));
            Console.ResetColor();
        }
        public static void WriteError(string errorCode)
        {
            string errorMessage = ErrorCodes.ContainsKey(errorCode) ? ErrorCodes[errorCode] : "Unknown error code.";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + errorMessage);
            Console.ResetColor();
        }

        #endregion

        #region Input Handling

        public static InputCollection GatherUserInput()
        {
            InputCollection userInputCollection = new(new List<string>());

            bool userCompletedInput = false;

            while (!userCompletedInput)
            {
                string? userInput = RequestUserInput_SingleLine("Instructions_RequestInput");

                if (userInput != null && userInput.Equals("done", StringComparison.OrdinalIgnoreCase))
                {
                    userCompletedInput = true;
                }
                else
                {
                    userInputCollection.addInput(userInput!);
                }
            }

            return userInputCollection;

        }

        #endregion
    }
}
