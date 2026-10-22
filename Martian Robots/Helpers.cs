using static Martian_Robots.Utilities;

namespace Martian_Robots
{
    public static class Helpers
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
        public static Instruction[] ParseInstructions(string input)
        {
            List<Instruction> instructions = new();
            foreach (char c in input)
            {
                if (Enum.TryParse(c.ToString(), out Instruction instruction))
                {
                    instructions.Add(instruction);
                }
                else
                {
                    throw new FormatException($"Invalid instruction character: {c}");
                }
            }
            return instructions.ToArray();
        }


        #endregion

        #region  Input Methods
        public static string? RequestUserInput_SingleLine(string messageCode)
        {
            WriteMessage(Messages[messageCode]);
            return Console.ReadLine();
        }

        #endregion

        #region Output Methods

        public static void WriteMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void WriteOutput(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
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

    }
}
