using static Martian_Robots.Helpers;
using static Martian_Robots.Utilities;

namespace Martian_Robots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Initilize
            InputCollection userInstructions = new(new List<string>());

            WriteMessage(Messages["Instructions_InputPreference_Individual"]);

            if (!Mars.isMarsInitialized())
            {
                Mars.BuildMarsMap();
            }

            #endregion

            userInstructions = GatherUserInput();

            WriteOutput($"Mars set successfully with boundaries: X:{Mars.Boundary.X}, Y:{Mars.Boundary.Y}");
        }
    }
}
