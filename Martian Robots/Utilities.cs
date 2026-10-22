namespace Martian_Robots
{
    public class Utilities
    {
        #region Data Structures

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
        {

            {
            }

        }

        #endregion

        #region Message Dictionaries
        public static Dictionary<string, string> Messages = new()
            {
                {"Mars_RequestInput", "Please enter the upper-right coordinates (max 50 50) of the Mars grid (e.g., '5 3')" },
            };

        public static Dictionary<string, string> ErrorCodes = new()
            {
                { "Mars_EmptyInput", "Input cannot be empty. Please provide coordinates." },
                { "Mars_InvalidCharacters", "Invalid characters detected. Please enter positive numbers for the coordinates." },
                { "Mars_InvalidFormat", "Invalid coordinate format. Please enter two positive numbers separated by a space." },
                { "Mars_OutOfBounds", "Coordinates are out of bounds. Please enter values for each coordinate between 0 and 50." },
            };

        #endregion

        #region  Input Methods


    }
}
