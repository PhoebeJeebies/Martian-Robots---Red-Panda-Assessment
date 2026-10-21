using static Martian_Robots.Utilities;
using static Martian_Robots.Mars;

namespace Martian_Robots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Initilize
                Mars.BuildMarsMap();
            #endregion

            WriteOutput($"Mars set successfully with boundaries: X:{Mars.Boundary.X}, Y:{Mars.Boundary.Y}");
        }
    }
}
