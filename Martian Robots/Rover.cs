using static Martian_Robots.Helpers;
using static Martian_Robots.Utilities;

namespace Martian_Robots
{
    public class Rover
    {
        public Coordinate Position;
        public Direction Orientation;
        // Define the cardinal points in clockwise order to be used for turning logic
        Direction[] cardinalPointsInProgression = new Direction[] { Direction.N, Direction.E, Direction.S, Direction.W };
        internal bool LostRoverFlag;


        public Rover(Coordinate startingPosition, Direction orientation)
        {
            Position = startingPosition;
            Orientation = orientation;
            LostRoverFlag = false;
        }

        #region Build

        public static Rover? BuildRover(string? input = null, UnitTestObject? testHook_Object = null)
        {
            Rover? rover = null;
            input = testHook_Object?.Input ?? input ?? string.Empty;

            // Prompt user for input if not provided
            if (string.IsNullOrWhiteSpace(input))
            {
                input = RequestUserInput_SingleLine("Rover_RequestInput");
            }

            // Validate the provided input for unit testing
            if (!ValidateUserInputForRover(input, out string? errorCode))
            {
                // Handle validation error
                WriteError(errorCode!);

                // Unit Testing - break the recursion if running in unit test mode
                if (!string.IsNullOrWhiteSpace(testHook_Object?.Input))
                {

                    // Set testOutput to the error code for unit testing
                    string errorMessage = ErrorCodes.ContainsKey(errorCode!) ? ErrorCodes[errorCode!] : "Unknown error code.";
                    testHook_Object.ErrorMessage = $"Error: {errorMessage}";

                    return null;
                }

            }
            else
            {
                // If the input is valid, parse and set the rover's position and orientation
                string[] parts = input!.Split(' ');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                Direction orientation = Enum.Parse<Direction>(parts[2], true);
                rover = new Rover(new Coordinate(x, y), orientation);
            }

            return rover;
        }

        #endregion

        #region Rover Specific Input Validation

        public static bool ValidateUserInputForRover(string? input, out string? errorCode)
        {
            errorCode = string.Empty;

            while (errorCode == string.Empty)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    errorCode = "Rover_Build_EmptyInput";
                    break;
                }
                string[] parts = input.Split(' ');
                if (parts.Length != 3)
                {
                    errorCode = "Rover_Build_InvalidFormat";
                    break;
                }
                if (!int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
                {
                    errorCode = "Rover_Build_InvalidCharacters";
                    break;
                }
                if (!Enum.TryParse(parts[2], true, out Direction orientation))
                {
                    errorCode = "Rover_Build_InvalidDirection";
                    break;
                }
                if (x < 0 || x > Mars.Boundary.X || y < 0 || y > Mars.Boundary.Y)
                {
                    errorCode = "Rover_Build_OutOfBounds";
                    break;
                }

                // If all checks pass, break the loop
                break;
            }

            // Return true if no error code was set
            return String.IsNullOrEmpty(errorCode);
        }

        public static bool ValidateUserInputForRoverInstructions(string? input, out string? errorCode)
        {
            errorCode = string.Empty;
            while (errorCode == string.Empty)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    errorCode = "Rover_Instructions_EmptyInput";
                    break;
                }
                foreach (char c in input)
                {
                    string token = c.ToString();
                    if (!Enum.GetNames(typeof(Instruction)).Contains(token, StringComparer.Ordinal))
                    {
                        errorCode = "Rover_Instructions_InvalidCharacters";
                        break;
                    }
                }
                // If all checks pass, break the loop
                break;
            }
            // Return true if no error code was set
            return String.IsNullOrEmpty(errorCode);
        }

        #endregion

        public static void BuildAndProcessRoversFromUserInputs(out List<string> output)
        {
            output = new List<string>();

            bool userCompletedInput = false;
            Rover? rover = null;
            HashSet<RoverScent> sessionScentsSet = new();

            WriteMessage(Messages["Instructions_RequestInput"], ConsoleColor.Cyan);

            while (!userCompletedInput)
            {
                string? userInput = null;

                // if no rover has been built yet, we are expecting the user to provide the rover's starting position and orientation or to indicate they are done. 
                if (rover == null)
                {
                    userInput = RequestUserInput_SingleLine("Rover_RequestInput");

                    if (userInput != null && userInput.Equals("done", StringComparison.OrdinalIgnoreCase))
                    {
                        userCompletedInput = true;
                    }
                    else
                    {
                        rover = Rover.BuildRover(userInput);

                        if (rover == null)
                        {
                            // If rover is still null after attempting to build, continue to the next iteration to prompt for input again
                            continue;
                        }
                    }
                }
                else
                {
                    // we can let the StartRoverMovement method handle the user input for the rover's movement instructions as we dont need access to the user input here
                    rover.ProcessRoverMovements(sessionScentsSet);
                    // After processing the rover's movements, we can add its final position and orientation to the output list
                    output.Add($"{rover.Position.X} {rover.Position.Y} {rover.Orientation}" + (rover.LostRoverFlag ? " LOST" : ""));
                    // current implementation does not support multiple rover instruction sequences, so we reset the rover after processing its instructions
                    rover = null; // Reset rover for the next one
                }
            }
        }

        #region Process Movement Instructions and Orientation Changes

        public void ProcessRoverMovements(HashSet<RoverScent>? sessionScentsSet, string? input = null, UnitTestObject? testHook_Object = null)
        {
            input = input ?? testHook_Object?.Input ?? string.Empty;

            // Prompt user for input if not provided an input for unit testing
            if (string.IsNullOrWhiteSpace(input))
            {
                input = RequestUserInput_SingleLine("Rover_RequestInstructions");
            }

            // Validate the provided input for unit testing
            if (!ValidateUserInputForRoverInstructions(input, out string? errorCode))
            {
                // Handle validation error
                WriteError(errorCode!);
                // unit testing - break the recursion if running in unit test mode
                if (!string.IsNullOrWhiteSpace(testHook_Object?.Input))
                {
                    // Set testOutput to the error code for unit testing
                    string errorMessage = ErrorCodes.ContainsKey(errorCode!) ? ErrorCodes[errorCode!] : "Unknown error code.";
                    testHook_Object.ErrorMessage = $"Error: {errorMessage}";
                    return;
                }
                // if the input is invalid, recursively call ProcessRoverMovements to prompt for input again
                ProcessRoverMovements(sessionScentsSet);
            }
            else
            {
                // If the input is valid, parse and process the instructions
                Instruction[] instructions = Helpers.ParseInstructions(input!);
                ProcessMoveInstructions(instructions, sessionScentsSet);
            }
        }

        public void ProcessMoveInstructions(Instruction[] instruction, HashSet<RoverScent>? sessionScentsSet)
        {
            foreach (Instruction command in instruction)
            {
                if (command == Instruction.F)
                {
                    MoveForward(sessionScentsSet);
                    if (LostRoverFlag)
                    {
                        // If the rover is lost, stop processing further instructions
                        break;
                    }
                }
                else if (command == Instruction.L || command == Instruction.R)
                {
                    Turn(command);
                }
            }
        }

        internal void MoveForward(HashSet<RoverScent>? sessionScentsSet)
        {
            Coordinate newPosition = Position;
            switch (Orientation)
            {
                case Direction.N:
                    newPosition.Y += 1;
                    break;
                case Direction.E:
                    newPosition.X += 1;
                    break;
                case Direction.S:
                    newPosition.Y -= 1;
                    break;
                case Direction.W:
                    newPosition.X -= 1;
                    break;
            }

            // If the current position and orientation have a scent, ignore the move command
            if (sessionScentsSet != null && sessionScentsSet.Any(s => s.Coordinate.X == Position.X && s.Coordinate.Y == Position.Y && s.Direction == Orientation))
            {
                return;
            }
            // Check if the new position is within the bounds of the Mars grid
            if (newPosition.X >= 0 && newPosition.X <= Mars.Boundary.X && newPosition.Y >= 0 && newPosition.Y <= Mars.Boundary.Y)
            {
                Position = newPosition;
            }
            else
            {
                // If the new position is out of bounds we can assume there isn't a scent at the current position and orientation as it would have returned earlier
                if (sessionScentsSet != null)
                {
                    // The rover is lost
                    sessionScentsSet.Add(new RoverScent(Position, Orientation));
                    LostRoverFlag = true;
                }
            }
        }

        internal void Turn(Instruction command)
        {
            int currentCardinalInProgressionIndex = Array.IndexOf(cardinalPointsInProgression, Orientation);

            if (command == Instruction.L)
            {
                currentCardinalInProgressionIndex = (currentCardinalInProgressionIndex - 1 + cardinalPointsInProgression.Length) % cardinalPointsInProgression.Length;
            }
            else if (command == Instruction.R)
            {
                currentCardinalInProgressionIndex = (currentCardinalInProgressionIndex + 1) % cardinalPointsInProgression.Length;
            }

            Orientation = cardinalPointsInProgression[currentCardinalInProgressionIndex];
        }

        #endregion
    }
}
