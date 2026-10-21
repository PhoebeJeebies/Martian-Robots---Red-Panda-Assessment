using Martian_Robots;
using static Martian_Robots.Mars;
using static Martian_Robots.Utilities;

namespace Martian_Robots_Tests
{
    [TestClass]
    public sealed class Martian_Robots_Tests
    {
        [TestCategory("Mars")]
        [TestClass]
        public sealed class MartianRobotsTests_Mars
        {
            [TestMethod]
            public void BuildMarsTest_ValidInput_ReturnsExpectedMarsGrid()
            {
                // Arrange
                string input = "5 3";
                Coordinate expectedCoordinate = new(5, 3);

                // Act
                BuildMarsMap(new UnitTestObject { Input = input });
                Coordinate ResultCoordinate = Mars.Boundary;

                // Assert
                Assert.AreEqual(expectedCoordinate, ResultCoordinate);
            }

            [TestMethod]
            public void BuildMarsTest_OutOfBoundsInput_ThrowsConsoleError()
            {
                // Arrange
                string input = "50 51";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Mars_OutOfBounds"];
                UnitTestObject unitTestObject = new() { Input = input };

                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    BuildMarsMap(unitTestObject);
                }

                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }

            [TestMethod]
            public void BuildMarsTest_InvalidInput_ThrowsConsoleError()
            {
                // Arrange
                string input = "Foobar";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Mars_InvalidCharacters"];
                UnitTestObject unitTestObject = new() { Input = input };

                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    BuildMarsMap(unitTestObject);
                }

                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }

            [TestMethod]
            public void BuildMarsTest_InvalidFormat_ThrowsConsoleError()
            {
                // Arrange
                string input = "1341";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Mars_InvalidFormat"];
                UnitTestObject unitTestObject = new() { Input = input };

                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    BuildMarsMap(unitTestObject);
                }

                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }
        }
    }
}
