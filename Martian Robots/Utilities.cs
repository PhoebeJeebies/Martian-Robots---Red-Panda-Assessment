namespace Martian_Robots
{
    public static class Utilities
    {
        #region Data Structures and Enums

        public struct Coordinate
        {
            private const int MinCoordinateValue = 0;
            private const int MaxCoordinateValue = 50;

            public int X { get; set; }
            public int Y { get; set; }
            public Coordinate(int x, int y)
            {
                X = Math.Max(MinCoordinateValue, Math.Min(x, MaxCoordinateValue));
                Y = Math.Max(MinCoordinateValue, Math.Min(y, MaxCoordinateValue));
            }
        }

        public class MarsMap
        {
            public Coordinate Boundary { get; set; }
            public MarsMap(Coordinate coordinate)
            {
                Boundary = coordinate;
            }
        }

        public class UnitTestObject
        {
            public string? Input { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public enum Direction
        {
            N,
            E,
            S,
            W
        }

        public enum Instruction
        {
            F,
            L,
            R
        }

        public class RoverScent
        {
            public Coordinate Coordinate { get; set; }
            public Direction Direction { get; set; }
            public RoverScent(Coordinate coordinate, Direction direction)
            {
                Coordinate = coordinate;
                Direction = direction;
            }
        }

        #endregion

        #region Message Dictionaries
        public static Dictionary<string, string> Messages = new()
            {
                {"Instructions_InputPreference_Individual", "You will be prompted for each input individually. Press Enter to submit your input." },
                {"Instructions_RequestInput", "Please enter instructions for the following prompts to build and move rovers. Once you have finished entering instruction sets type 'done' to get the results." },
                {"Mars_RequestInput", "Please enter the upper-right coordinates (max 50 50) of the Mars grid (e.g., '5 3')" },
                {"Rover_RequestInput", "Please enter the starting coordinates and starting orientation for a rover (e.g., '1 2 N')" },
                {"Rover_RequestInstructions", "Please enter the movement instructions for the rover (e.g., 'LFRFRR')" },
            };

        public static Dictionary<string, string> ErrorCodes = new()
            {
                { "Mars_EmptyInput", "Input cannot be empty. Please provide coordinates." },
                { "Mars_InvalidCharacters", "Invalid characters detected. Please enter positive numbers for the coordinates." },
                { "Mars_InvalidFormat", "Invalid coordinate format. Please enter two positive numbers separated by a space." },
                { "Mars_OutOfBounds", "Coordinates are out of bounds. Please enter values for each coordinate between 0 and 50." },
                { "Rover_Build_EmptyInput", "Input cannot be empty. Please provide starting coordinates and a starting orientation." },
                { "Rover_Build_InvalidCharacters", "Invalid characters detected. Please enter only numbers for the starting coordinates." },
                { "Rover_Build_InvalidFormat", "Invalid characters detected. Please enter two sets of positive numbers separated by a space followed by a cardinal direction" },
                { "Rover_Build_OutOfBounds", "Starting coordinates are out of bounds of Mars. Please enter values within the bounds you specified for Mars." },
                { "Rover_Build_InvalidDirection", "Invalid direction. Please enter N, E, S, or W." },
                { "Rover_Instructions_EmptyInput", "Input cannot be empty. Please provide movement instructions for the rover." },
                { "Rover_Instructions_InvalidCharacters", "Invalid instruction detected. Please enter only L, R, or F for the movement instructions." },
            };

        #endregion


    }
}
