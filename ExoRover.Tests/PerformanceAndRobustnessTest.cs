using Map;
using Rover;
using Xunit;
using System.Diagnostics;

namespace ExoRover.Tests;

public class PerformanceAndRobustnessTest
{
    [Fact]
    public void Map_LargeNumberOfObstacles_ShouldPerformAcceptably()
    {
        var map = new Map.Map();
        var stopwatch = Stopwatch.StartNew();

        RandomObstacleGenerator.GenerateObstacles(map, 50);

        stopwatch.Stop();
        Assert.True(stopwatch.ElapsedMilliseconds < 1000, "Generation should complete within 1 second");
        
        int obstacleCount = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map.hasObstacle(x, y))
                    obstacleCount++;
            }
        }
        
        Assert.Equal(50, obstacleCount);
    }

    [Fact]
    public void Orientation_ManyRotations_ShouldMaintainConsistency()
    {
        var startOrientation = Orientation.Nord;
        var currentOrientation = startOrientation;

        for (int i = 0; i < 1000; i++)
        {
            currentOrientation = currentOrientation.RotationHoraire();
        }

        Assert.Equal(startOrientation, currentOrientation);
    }

    [Fact]
    public void Position_ManyMovements_ShouldWrapCorrectly()
    {
        var position = new Position(5, 5);
        var orientation = Orientation.Est;

        for (int i = 0; i < 50; i++)
        {
            position = orientation.Avancer(position);
        }

        Assert.Equal(5, position.Longitude);
        Assert.Equal(5, position.Latitude);
    }

    [Fact]
    public void Command_ManyAdditions_ShouldConcatenateCorrectly()
    {
        var command = Command.Avancer;

        for (int i = 0; i < 100; i++)
        {
            command = command + Command.TournerADroite;
        }

        var result = command.ToString();
        Assert.True(result.Length == 101);
        Assert.True(result.StartsWith("A"));
        Assert.True(result.EndsWith("D"));
        
        int dCount = result.Count(c => c == 'D');
        Assert.Equal(100, dCount);
    }

    [Fact]
    public void Map_HasObstacle_ManyChecks_ShouldBePerformant()
    {
        var map = new Map.Map();
        RandomObstacleGenerator.GenerateObstacles(map, 30);
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < 10000; i++)
        {
            int x = i % 10;
            int y = (i / 10) % 10;
            map.hasObstacle(x, y);
        }

        stopwatch.Stop();
        Assert.True(stopwatch.ElapsedMilliseconds < 100, "Many obstacle checks should complete quickly");
    }

    [Fact]
    public void RandomObstacleGenerator_Consistency_ShouldProduceDifferentResults()
    {
        var map1 = new Map.Map();
        var map2 = new Map.Map();

        RandomObstacleGenerator.GenerateObstacles(map1, 10);
        RandomObstacleGenerator.GenerateObstacles(map2, 10);

        bool mapsAreDifferent = false;
        for (int x = 0; x < 10 && !mapsAreDifferent; x++)
        {
            for (int y = 0; y < 10 && !mapsAreDifferent; y++)
            {
                if (map1.hasObstacle(x, y) != map2.hasObstacle(x, y))
                {
                    mapsAreDifferent = true;
                }
            }
        }

        Assert.True(mapsAreDifferent, "Two randomly generated maps should be different");
    }

    [Fact]
    public void Orientation_ToString_ShouldBeConsistent()
    {
        var orientations = new[] { Orientation.Nord, Orientation.Sud, Orientation.Est, Orientation.Ouest };
        var expectedStrings = new[] { "Nord", "Sud", "Est", "Ouest" };

        for (int i = 0; i < orientations.Length; i++)
        {
            Assert.Equal(expectedStrings[i], orientations[i].ToString());
        }
    }
}
