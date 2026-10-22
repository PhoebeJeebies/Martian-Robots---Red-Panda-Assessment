
using static Martian_Robots.Helpers;
using static Martian_Robots.Utilities;

namespace Martian_Robots
{
    public class Mars
    {
        public static Coordinate Boundary { get; set; }
        public Mars(Coordinate boundary)
        {
            Boundary = boundary;
        }

        public static bool isMarsInitialized()
        {
            if (Boundary.X > 0 && Boundary.Y > 0)
            {
                return true;
            }

            return false;
        }

        internal static string GetValidatedUserInput(UnitTestObject? testHook_Object = null)
        {
            // Prompt user for input, or use test object input if provided
            string? input = testHook_Object?.Input ?? RequestUserInput_SingleLine("Mars_RequestInput");

            if (!ValidateUserInputForMarsMap(input, out string? errorCode))
            {
                // Handle validation error
                WriteError(errorCode!);

                // Unit Testing - break the recursion if running in unit test mode and an error occurs
                if (!string.IsNullOrWhiteSpace(testHook_Object?.Input))
                {
                    // Set testOutput to the error code for unit testing
                    string errorMessage = ErrorCodes.ContainsKey(errorCode!) ? ErrorCodes[errorCode!] : "Unknown error code.";
                    testHook_Object.ErrorMessage = $"Error: {errorMessage}";
                    return string.Empty;
                }

                // if the input is invalid, recursively call "GetValidatedUserInput" to prompt for input again
                input = GetValidatedUserInput();
            }

            return input!;
        }


        public static void BuildMarsMap(UnitTestObject? testHook_Object = null)
        {
            // Prompt user for input and validate it, will return early if there is an error during validation for unit testing scenarios
            string validatedInput = GetValidatedUserInput(testHook_Object);

            if (testHook_Object?.Input != null && !string.IsNullOrEmpty(testHook_Object.ErrorMessage))
            {
                // If there was an error during validation, return early for unit testing scenarios
                return;
            }

            // once the input is valid, parse and set the mars map
            new Mars(ParseCoordinate(validatedInput));
        }


        // Section - Mars Specific Input Validation
        internal static bool ValidateUserInputForMarsMap(string? input, out string? errorCode)
        {
            errorCode = string.Empty;

            while (string.IsNullOrEmpty(errorCode))
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    errorCode = "Mars_EmptyInput";
                    break;
                }
                input = input!.Trim();
                if (input.Any(c => !char.IsDigit(c) && c != ' '))
                {
                    errorCode = "Mars_InvalidCharacters";
                    break;
                }
                if (input.Count(c => c == ' ') != 1 || input.Split(' ').Length != 2)
                {
                    errorCode = "Mars_InvalidFormat";
                    break;
                }
                if (int.TryParse(input.Split(' ')[0], out int x) && int.TryParse(input.Split(' ')[1], out int y))
                {
                    if (x > 50 || y > 50)
                    {
                        errorCode = "Mars_OutOfBounds";
                        break;
                    }
                }

                // If all checks pass, break the loop
                break;
            }

            // Return true if no error code was set
            return String.IsNullOrEmpty(errorCode);
        }
    }
}
