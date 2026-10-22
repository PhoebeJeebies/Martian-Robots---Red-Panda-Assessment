using Martian_Robots;
using static Martian_Robots.Mars;
using static Martian_Robots.Rover;
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


        [TestCategory("Rover")]
        [TestClass]
        public sealed class MartianRobotsTests_Rover
        {
            [TestMethod]
            public void BuildRoverTest_ValidInput_ReturnsExpectedRover()
            {
                // Arrange
                string input = "1 2 N";
                Coordinate expectedCoordinate = new(1, 2);
                Direction expectedDirection = Direction.N;
                Rover expectedRover = new(expectedCoordinate, expectedDirection);
                Rover? rover = null;
                UnitTestObject unitTestObject = new() { Input = input };

                // Act
                rover = BuildRover(5, 3, null, unitTestObject);

                // Assert
                Assert.AreEqual(expectedCoordinate, rover?.Position);
                Assert.AreEqual(expectedDirection, rover?.Orientation);
            }

            [TestMethod]
            public void BuildRoverTest_InvalidInput_ThrowsConsoleError()
            {
                // Arrange
                string input = "10 2o N";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Rover_Build_InvalidCharacters"];
                string receivedErrorMessage = string.Empty;
                UnitTestObject unitTestObject = new() { Input = input };

                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    BuildRover(5, 3, null, unitTestObject);
                }
                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }

            [TestMethod]
            public void BuildRoverTest_InvalidFormat_ThrowsConsoleError()
            {
                // Arrange
                string input = "1 2";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Rover_Build_InvalidFormat"];
                string receivedErrorMessage = string.Empty;
                UnitTestObject unitTestObject = new() { Input = input };
                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    BuildRover(5, 3, null, unitTestObject);
                }
                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }

            [TestMethod]
            public void BuildRoverTest_InvalidDirection_ThrowsConsoleError()
            {
                // Arrange
                string input = "1 2 SW";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Rover_Build_InvalidDirection"];
                string receivedErrorMessage = string.Empty;
                UnitTestObject unitTestObject = new() { Input = input };
                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    BuildRover(5, 3, null, unitTestObject);
                }
                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }

            [TestMethod]
            public void BuildRoverTest_OutOfBoundsInput_ThrowsConsoleError()
            {
                // Arrange
                string input = "6 2 N";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Rover_Build_OutOfBounds"];
                string receivedErrorMessage = string.Empty;
                UnitTestObject unitTestObject = new() { Input = input };
                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    BuildRover(5, 3, null, unitTestObject);
                }
                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }

            [TestMethod]
            public void MoveRoverTest_ValidInstructions_ReturnsExpectedPositionAndOrientation()
            {
                // Arrange
                Coordinate initialCoordinate = new(1, 2);
                Direction initialDirection = Direction.N;
                Rover rover = new(initialCoordinate, initialDirection);
                string instructions = "LFRFRR";
                Coordinate expectedCoordinate = new(0, 3);
                Direction expectedDirection = Direction.S;
                UnitTestObject unitTestObject = new() { Input = instructions };

                // Act
                rover.ProcessRoverMovements(50, 50, null, null, unitTestObject);

                // Assert
                Assert.AreEqual(expectedCoordinate, rover.Position);
                Assert.AreEqual(expectedDirection, rover.Orientation);
            }

            [TestMethod]
            public void MoveRoverTest_InvalidInstructions_ThrowsConsoleError()
            {
                // Arrange
                Coordinate initialCoordinate = new(1, 2);
                Direction initialDirection = Direction.N;
                Rover rover = new(initialCoordinate, initialDirection);
                string instructions = "LFRXRR";
                string expectedErrorMessage = "Error: " + Utilities.ErrorCodes["Rover_Instructions_InvalidCharacters"];
                string receivedErrorMessage = string.Empty;
                UnitTestObject unitTestObject = new() { Input = instructions };

                // Act
                while (String.IsNullOrEmpty(unitTestObject.ErrorMessage))
                {
                    rover.ProcessRoverMovements(50, 50, null, null, unitTestObject);
                }

                // Assert
                Assert.AreEqual(expectedErrorMessage, unitTestObject.ErrorMessage);
            }
        }
    }
}
