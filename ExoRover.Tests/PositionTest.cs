using Map;
using Xunit;

namespace ExoRover.Tests;

public class PositionTest
{
    [Fact]
    public void Position_DefaultConstructor_ShouldInitializeToDefaultValues()
    {
        var position = new Position();

        Assert.Equal(5, position.Longitude);
        Assert.Equal(5, position.Latitude);
    }

    [Fact]
    public void Position_ParameterizedConstructor_ShouldInitializeCorrectly()
    {
        int longitude = 10;
        int latitude  = 20;

        var position = new Position(longitude, latitude);

        Assert.Equal(longitude, position.Longitude);
        Assert.Equal(latitude, position.Latitude);
    }

    [Fact]
    public void Position_SetProperties_ShouldUpdateValues()
    {
        var position = new Position();

        position.Longitude = 15;
        position.Latitude  = 25;

        Assert.Equal(15, position.Longitude);
        Assert.Equal(25, position.Latitude);
    }

    [Fact]
    public void Position_NegativeValues_ShouldBeAllowed()
    {
        var position = new Position(-5, -10);

        Assert.Equal(-5, position.Longitude);
        Assert.Equal(-10, position.Latitude);
    }
}