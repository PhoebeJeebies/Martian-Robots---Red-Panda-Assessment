using static Martian_Robots.Helpers;
using static Martian_Robots.Utilities;

namespace Martian_Robots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Initilize
            List<string> roverDestinationOutput = new();

            WriteMessage(Messages["App_Start"] + "\n Version " + typeof(Program).Assembly.GetName().Version, ConsoleColor.Cyan);
            WriteMessage(Messages["Instructions_InputPreference_Individual"]);

            if (!Mars.isMarsInitialized())
            {
                Mars.BuildMarsMap();
            }

            #endregion

            while (roverDestinationOutput.Count == 0)
            {
                Rover.BuildAndProcessRoversFromUserInputs(out roverDestinationOutput, Mars.Boundary.X, Mars.Boundary.Y);
            }

            if (roverDestinationOutput.Count > 0)
            {
                WriteMessage("Rover Destinations Output: ", ConsoleColor.Cyan);
                foreach (string output in roverDestinationOutput)
                {
                    WriteOutput(output);
                }
            }
        }
    }
}
