using ExoRover;
using Xunit;

namespace ExoRover.Tests;

public class PointTest
{
    [Fact]
    public void Point_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        int x = 5;
        int y = 10;

        // Act
        var point = new Point(x, y);

        // Assert
        Assert.Equal(x, point.X);
        Assert.Equal(y, point.Y);
    }

    [Fact]
    public void Point_Equality_ShouldWorkCorrectly()
    {
        // Arrange
        var point1 = new Point(3, 7);
        var point2 = new Point(3, 7);
        var point3 = new Point(4, 7);

        // Assert
        Assert.Equal(point1, point2);
        Assert.NotEqual(point1, point3);
    }

    [Fact]
    public void Point_ToString_ShouldReturnCorrectFormat()
    {
        // Arrange
        var point = new Point(2, 8);

        // Act
        var result = point.ToString();

        // Assert
        Assert.Contains("2", result);
        Assert.Contains("8", result);
    }

    [Fact]
    public void Point_NegativeCoordinates_ShouldBeAllowed()
    {
        // Act
        var point = new Point(-3, -5);

        // Assert
        Assert.Equal(-3, point.X);
        Assert.Equal(-5, point.Y);
    }
}
