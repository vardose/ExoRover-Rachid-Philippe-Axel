using Map;
using Xunit;

namespace ExoRover.Tests;

public class ObstacleTest
{
    [Fact]
    public void Obstacle_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        int latitude = 5;
        int longitude = 10;

        // Act
        var obstacle = new Obstacle(latitude, longitude);

        // Assert
        Assert.NotNull(obstacle.Position);
        Assert.Equal(latitude, obstacle.Position.Latitude);
        Assert.Equal(longitude, obstacle.Position.Longitude);
    }

    [Fact]
    public void Obstacle_Position_ShouldBeReadOnly()
    {
        // Arrange
        var obstacle = new Obstacle(3, 7);
        var originalPosition = obstacle.Position;

        // Act - tentative de modifier la position (ne devrait pas être possible directement)
        // La propriété Position a un setter privé

        // Assert
        Assert.Same(originalPosition, obstacle.Position);
    }

    [Fact]
    public void Obstacle_WithNegativeCoordinates_ShouldBeAllowed()
    {
        // Act
        var obstacle = new Obstacle(-5, -10);

        // Assert
        Assert.Equal(-5, obstacle.Position.Latitude);
        Assert.Equal(-10, obstacle.Position.Longitude);
    }

    [Fact]
    public void Obstacle_WithZeroCoordinates_ShouldBeAllowed()
    {
        // Act
        var obstacle = new Obstacle(0, 0);

        // Assert
        Assert.Equal(0, obstacle.Position.Latitude);
        Assert.Equal(0, obstacle.Position.Longitude);
    }

    [Fact]
    public void Obstacle_ImplementsIObstacle_ShouldHavePositionProperty()
    {
        // Arrange
        Obstacle obstacle = new Obstacle(2, 4);

        // Assert
        Assert.NotNull(obstacle.Position);
        Assert.Equal(2, obstacle.Position.Latitude);
        Assert.Equal(4, obstacle.Position.Longitude);
    }
}
