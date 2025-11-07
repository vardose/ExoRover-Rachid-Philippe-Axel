using Map;
using Xunit;

namespace ExoRover.Tests;

public class MapTest
{
    [Fact]
    public void Map_Constructor_ShouldInitializeEmptyMap()
    {
        var map = new Map.Map();

        Assert.NotNull(map);
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Assert.False(map.hasObstacle(x, y));
            }
        }
    }

    [Fact]
    public void Map_AddObstacle_ShouldPlaceObstacleCorrectly()
    {
        var map      = new Map.Map();
        var obstacle = new Obstacle(latitude: 3, longitude: 5);

        map.addObstacle(obstacle);

        Assert.True(map.hasObstacle(5, 3)); // longitude = x, latitude = y
    }

    [Fact]
    public void Map_AddMultipleObstacles_ShouldPlaceAllCorrectly()
    {
        var map       = new Map.Map();
        var obstacle1 = new Obstacle(latitude: 1, longitude: 2);
        var obstacle2 = new Obstacle(latitude: 5, longitude: 7);

        map.addObstacle(obstacle1);
        map.addObstacle(obstacle2);

        Assert.True(map.hasObstacle(2, 1));
        Assert.True(map.hasObstacle(7, 5));
        Assert.False(map.hasObstacle(0, 0)); // Pas d'obstacle ici
    }

    [Fact]
    public void Map_AddObstacle_OutOfBounds_ShouldThrowArgumentOutOfRangeException()
    {
        var map                 = new Map.Map();
        var obstacleOutOfBounds = new Obstacle(latitude: 15, longitude: 20); // Hors limites

        Assert.Throws<ArgumentOutOfRangeException>(() => map.addObstacle(obstacleOutOfBounds));
    }

    [Fact]
    public void Map_AddObstacle_NegativeCoordinates_ShouldThrowArgumentOutOfRangeException()
    {
        var map              = new Map.Map();
        var obstacleNegative = new Obstacle(latitude: -1, longitude: -1);

        Assert.Throws<ArgumentOutOfRangeException>(() => map.addObstacle(obstacleNegative));
    }

    [Fact]
    public void Map_HasObstacle_OutOfBounds_ShouldReturnFalse()
    {
        var map = new Map.Map();

        Assert.False(map.hasObstacle(-1, -1));
        Assert.False(map.hasObstacle(10, 10));
        Assert.False(map.hasObstacle(15, 5));
        Assert.False(map.hasObstacle(5, 15));
    }

    [Fact]
    public void Map_HasObstacle_ValidEmptyPosition_ShouldReturnFalse()
    {
        var map = new Map.Map();

        Assert.False(map.hasObstacle(5, 5));
        Assert.False(map.hasObstacle(0, 0));
        Assert.False(map.hasObstacle(9, 9));
    }

    [Fact]
    public void Map_ImplementsIMap_ShouldHaveCorrectMethods()
    {
        Map.Map map      = new Map.Map();
        var     obstacle = new Obstacle(latitude: 2, longitude: 3);

        map.addObstacle(obstacle);

        Assert.True(map.hasObstacle(3, 2));
    }

    [Fact]
    public void Map_AddObstacle_AtBoundaries_ShouldWork()
    {
        var map       = new Map.Map();
        var obstacle1 = new Obstacle(latitude: 0, longitude: 0); // Coin supérieur gauche
        var obstacle2 = new Obstacle(latitude: 9, longitude: 9); // Coin inférieur droit
        var obstacle3 = new Obstacle(latitude: 0, longitude: 9); // Coin supérieur droit
        var obstacle4 = new Obstacle(latitude: 9, longitude: 0); // Coin inférieur gauche

        map.addObstacle(obstacle1);
        map.addObstacle(obstacle2);
        map.addObstacle(obstacle3);
        map.addObstacle(obstacle4);

        Assert.True(map.hasObstacle(0, 0));
        Assert.True(map.hasObstacle(9, 9));
        Assert.True(map.hasObstacle(9, 0));
        Assert.True(map.hasObstacle(0, 9));
    }
}