using Rover;
using Xunit;

namespace ExoRover.Tests;

public class CommandTest
{
    [Fact]
    public void Command_ToString_ShouldReturnCorrectRepresentation()
    {
        Assert.Equal("A", Command.Avancer.ToString());
        Assert.Equal("R", Command.Reculer.ToString());
        Assert.Equal("G", Command.TournerAGauche.ToString());
        Assert.Equal("D", Command.TournerADroite.ToString());
    }

    [Fact]
    public void Command_Addition_ShouldConcatenateCommands()
    {
        var command1 = Command.Avancer;
        var command2 = Command.TournerADroite;

        var result = command1 + command2;

        Assert.Equal("AD", result.ToString());
    }

    [Fact]
    public void Command_MultipleAddition_ShouldConcatenateAllCommands()
    {
        var result = Command.TournerAGauche + Command.TournerADroite + Command.Reculer + Command.Avancer;

        Assert.Equal("GDRA", result.ToString());
    }

    [Fact]
    public void Command_StaticInstances_ShouldBeUnique()
    {
        Assert.NotSame(Command.Avancer, Command.Reculer);
        Assert.NotSame(Command.TournerAGauche, Command.TournerADroite);
        Assert.Same(Command.Avancer, Command.Avancer);
        Assert.Same(Command.Reculer, Command.Reculer);
    }

    [Fact]
    public void Command_EmptyAddition_ShouldHandleCorrectly()
    {
        var command = Command.Avancer;

        var result = command + command;

        Assert.Equal("AA", result.ToString());
    }
}