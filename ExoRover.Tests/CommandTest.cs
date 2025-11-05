using ExoRover;

using Xunit;

namespace ExoRover.Tests;


public class CommandTest
{
    [Fact]
    public void Command_ToString_ShouldReturnCorrectRepresentation()
    {
        // Arrange & Act & Assert
        Assert.Equal("A", Command.Avancer.ToString());
        Assert.Equal("R", Command.Reculer.ToString());
        Assert.Equal("G", Command.TournerAGauche.ToString());
        Assert.Equal("D", Command.TournerADroite.ToString());
    }

    [Fact]
    public void Command_Addition_ShouldConcatenateCommands()
    {
        // Arrange
        var command1 = Command.Avancer;
        var command2 = Command.TournerADroite;
        
        // Act
        var result = command1 + command2;
        
        // Assert
        Assert.Equal("AD", result.ToString());
    }

    [Fact]
    public void Command_MultipleAddition_ShouldConcatenateAllCommands()
    {
        // Arrange & Act
        var result = Command.TournerAGauche + Command.TournerADroite + Command.Reculer + Command.Avancer;
        
        // Assert
        Assert.Equal("GDRA", result.ToString());
    }
}