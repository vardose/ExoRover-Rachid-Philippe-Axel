using Map;
using Rover;
using MissionControl;
using Xunit;

namespace ExoRover.Tests;

public class IntegrationTest
{
    [Fact]
    public void Rover_ExecuteCommand_Avancer_ShouldUpdatePosition()
    {

        var config = CreateTestConfig();
        var rover  = new Rover.Rover(config);

        Assert.NotNull(rover);
    }

    [Fact]
    public void Map_And_Obstacle_Integration_ShouldWork()
    {
        var map       = new Map.Map();
        var obstacle1 = new Obstacle(2, 3);
        var obstacle2 = new Obstacle(5, 7);

        map.addObstacle(obstacle1);
        map.addObstacle(obstacle2);

        Assert.True(map.hasObstacle(3, 2)); // longitude=x, latitude=y
        Assert.True(map.hasObstacle(7, 5));
        Assert.False(map.hasObstacle(0, 0));
    }

    [Fact]
    public void RandomObstacleGenerator_And_Map_Integration_ShouldWork()
    {
        var map = new Map.Map();

        RandomObstacleGenerator.GenerateObstacles(map, 5);

        int obstacleCount = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map.hasObstacle(x, y))
                    obstacleCount++;
            }
        }

        Assert.Equal(5, obstacleCount);
    }

    [Fact]
    public void Orientation_Command_Flow_ShouldWork()
    {
        var startPoint  = new Position(5, 5);
        var orientation = Orientation.Nord;

        var afterMove      = orientation.Avancer(startPoint);   // (5, 4)
        var newOrientation = orientation.RotationHoraire();     // Est
        var afterTurn      = newOrientation.Avancer(afterMove); // (6, 4)

        Assert.Equal(5, afterMove.Longitude);
        Assert.Equal(4, afterMove.Latitude);
        Assert.Equal(Orientation.Est, newOrientation);
        Assert.Equal(6, afterTurn.Longitude);
        Assert.Equal(4, afterTurn.Latitude);
    }

    [Fact]
    public void MapRenderer_Integration_ShouldRenderWithoutError()
    {
        var map      = new Map.Map();
        var obstacle = new Obstacle(2, 3);
        map.addObstacle(obstacle);

        var renderer = new MapRenderer();
        renderer.RoverX = 1;
        renderer.RoverY = 1;

        renderer.Render(map);
    }

    private Config CreateTestConfig()
    {
        return new Config
        {
            Communication = new CommunicationConfig
            {
                Host               = "127.0.0.1",
                MissionControlPort = 8080,
                RoverPort          = 8081
            },
            RoverSettings = new RoverSettingsConfig
            {
                Orientation        = "Nord",
                InitialPosition    = new List<int> { 0, 0 },
                isObstacleDetected = false
            }
        };
    }
}