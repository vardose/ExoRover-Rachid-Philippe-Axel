using Map;
using Xunit;

namespace ExoRover.Tests;

public class PositionTest
{
    [Fact]
    public void Position_DefaultConstructor_ShouldInitializeToZero()
    {
        // Act
        var position = new Position();

        // Assert
        Assert.Equal(0, position.Longitude);
        Assert.Equal(0, position.Latitude);
    }

    [Fact]
    public void Position_ParameterizedConstructor_ShouldInitializeCorrectly()
    {
        // Arrange
        int longitude = 10;
        int latitude = 20;

        // Act
        var position = new Position(longitude, latitude);

        // Assert
        Assert.Equal(longitude, position.Longitude);
        Assert.Equal(latitude, position.Latitude);
    }

    [Fact]
    public void Position_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var position = new Position();

        // Act
        position.Longitude = 15;
        position.Latitude = 25;

        // Assert
        Assert.Equal(15, position.Longitude);
        Assert.Equal(25, position.Latitude);
    }

    [Fact]
    public void Position_NegativeValues_ShouldBeAllowed()
    {
        // Act
        var position = new Position(-5, -10);

        // Assert
        Assert.Equal(-5, position.Longitude);
        Assert.Equal(-10, position.Latitude);
    }
}
