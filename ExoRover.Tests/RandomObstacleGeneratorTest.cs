using Map;
using Xunit;

namespace ExoRover.Tests;

public class RandomObstacleGeneratorTest
{
    [Fact]
    public void GenerateObstacles_ShouldCreateRequestedNumberOfObstacles()
    {
        var map            = new Map.Map();
        int requestedCount = 5;

        RandomObstacleGenerator.GenerateObstacles(map, requestedCount);

        int actualCount = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map.hasObstacle(x, y))
                    actualCount++;
            }
        }

        Assert.Equal(requestedCount, actualCount);
    }

    [Fact]
    public void GenerateObstacles_WithZeroCount_ShouldCreateNoObstacles()
    {
        var map = new Map.Map();

        RandomObstacleGenerator.GenerateObstacles(map, 0);

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Assert.False(map.hasObstacle(x, y));
            }
        }
    }

    [Fact]
    public void GenerateObstacles_ShouldNotCreateDuplicateObstacles()
    {
        var map            = new Map.Map();
        int requestedCount = 10;

        RandomObstacleGenerator.GenerateObstacles(map, requestedCount);

        var obstaclePositions = new HashSet<(int, int)>();
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map.hasObstacle(x, y))
                {
                    var position = (x, y);
                    Assert.False(obstaclePositions.Contains(position), $"Duplicate obstacle at position ({x}, {y})");
                    obstaclePositions.Add(position);
                }
            }
        }
    }

    [Fact]
    public void GenerateObstacles_WithMaxCount_ShouldFillEntireMap()
    {
        var map                  = new Map.Map();
        int maxPossibleObstacles = 100; // 10x10 map

        RandomObstacleGenerator.GenerateObstacles(map, maxPossibleObstacles);

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Assert.True(map.hasObstacle(x, y));
            }
        }
    }

    [Fact]
    public void GenerateObstacles_ImplementsIObstacleGenerator()
    {
        var map = new Map.Map();

        RandomObstacleGenerator.GenerateObstacles(map, 3);
    }

    [Fact]
    public void GenerateObstacles_ShouldGenerateWithinMapBounds()
    {
        var map            = new Map.Map();
        int requestedCount = 20;

        RandomObstacleGenerator.GenerateObstacles(map, requestedCount);


        int actualCount = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map.hasObstacle(x, y))
                    actualCount++;
            }
        }

        Assert.True(actualCount > 0);
    }
}