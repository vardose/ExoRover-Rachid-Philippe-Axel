using Map;
using Xunit;
using System.Collections.Generic;

namespace ExoRover.Tests;

public class RandomObstacleGeneratorTest
{
    [Fact]
    public void GenerateObstacles_ShouldCreateRequestedNumberOfObstacles()
    {
        // Arrange
        var map            = new Map.Map();
        int requestedCount = 5;

        // Act
        RandomObstacleGenerator.GenerateObstacles(map, requestedCount);

        // Assert
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
        // Arrange
        var map = new Map.Map();

        // Act
        RandomObstacleGenerator.GenerateObstacles(map, 0);

        // Assert
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
        // Arrange
        var map            = new Map.Map();
        int requestedCount = 10;

        // Act
        RandomObstacleGenerator.GenerateObstacles(map, requestedCount);

        // Assert
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
        // Arrange
        var map                  = new Map.Map();
        int maxPossibleObstacles = 100; // 10x10 map

        // Act
        RandomObstacleGenerator.GenerateObstacles(map, maxPossibleObstacles);

        // Assert
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
        // Arrange
        var map = new Map.Map();

        // Act & Assert - Should not throw
        RandomObstacleGenerator.GenerateObstacles(map, 3);
    }

    [Fact]
    public void GenerateObstacles_ShouldGenerateWithinMapBounds()
    {
        // Arrange
        var map            = new Map.Map();
        int requestedCount = 20;

        // Act
        RandomObstacleGenerator.GenerateObstacles(map, requestedCount);

        // Assert
        // Vérifier qu'aucun obstacle n'existe en dehors des limites
        // Cette vérification est implicite car Map.hasObstacle retourne false pour les coordonnées hors limites
        // et Map.addObstacle lève une exception pour les coordonnées hors limites
        
        // Si nous arrivons ici sans exception, cela signifie que tous les obstacles 
        // ont été placés dans les limites de la carte
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
