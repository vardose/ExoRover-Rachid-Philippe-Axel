using Map;
using Rover;
using Xunit;
using System.Text.Json;

namespace ExoRover.Tests;

public class EdgeCasesAndErrorHandlingTest : IDisposable
{
    private readonly string _testConfigPath = "test_edge_cases_config.json";

    public EdgeCasesAndErrorHandlingTest()
    {
        var configData = new
        {
            Communication = new
            {
                Host = "127.0.0.1",
                MissionControlPort = 8080,
                RoverPort = 8081
            },
            RoverSettings = new
            {
                Orientation = "Nord",
                InitialPosition = new[] { 0, 0 },
                isObstacleDetected = false
            }
        };

        var json = JsonSerializer.Serialize(configData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_testConfigPath, json);
    }

    public void Dispose()
    {
        if (File.Exists(_testConfigPath))
        {
            File.Delete(_testConfigPath);
        }
    }

    [Fact]
    public void Map_AddObstacle_SamePositionTwice_ShouldNotCauseDuplicates()
    {
        var map = new Map.Map();
        var obstacle1 = new Obstacle(2, 3);
        var obstacle2 = new Obstacle(2, 3); // Même position

        map.addObstacle(obstacle1);
        map.addObstacle(obstacle2); // Ne devrait pas causer d'erreur

        Assert.True(map.hasObstacle(3, 2));
        
        int obstacleCount = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map.hasObstacle(x, y))
                    obstacleCount++;
            }
        }
        Assert.Equal(1, obstacleCount);
    }

    [Fact]
    public void Orientation_InvalidRotation_ShouldWorkWithValidOrientations()
    {
        Assert.Equal(Orientation.Est, Orientation.Nord.RotationHoraire());
        Assert.Equal(Orientation.Sud, Orientation.Est.RotationHoraire());
        Assert.Equal(Orientation.Ouest, Orientation.Sud.RotationHoraire());
        Assert.Equal(Orientation.Nord, Orientation.Ouest.RotationHoraire());
    }

    [Fact]
    public void Position_ExtremeValues_ShouldBeHandled()
    {
        var extremePosition = new Position(int.MaxValue, int.MinValue);

        Assert.Equal(int.MaxValue, extremePosition.Longitude);
        Assert.Equal(int.MinValue, extremePosition.Latitude);
    }

    [Fact]
    public void RandomObstacleGenerator_MoreObstaclesThanCells_ShouldFillMap()
    {
        var map = new Map.Map();
        int impossibleCount = 150; // Plus que les 100 cellules disponibles

        RandomObstacleGenerator.GenerateObstacles(map, impossibleCount);

        int actualCount = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map.hasObstacle(x, y))
                    actualCount++;
            }
        }

        Assert.Equal(100, actualCount); // Devrait être limité à 100
    }

    [Fact]
    public void Config_PartialConfiguration_ShouldStillLoad()
    {
        var partialConfig = @"{
            ""Communication"": {
                ""Host"": ""127.0.0.1""
            }
        }";
        
        var partialConfigPath = "partial_config.json";
        File.WriteAllText(partialConfigPath, partialConfig);

        var config = Config.Load(partialConfigPath);

        Assert.NotNull(config);
        Assert.NotNull(config.Communication);
        Assert.Equal("127.0.0.1", config.Communication.Host);
        Assert.Equal(0, config.Communication.MissionControlPort); // Valeur par défaut
        
        File.Delete(partialConfigPath);
    }

    [Fact]
    public void Obstacle_Constructor_ParameterOrder_ShouldBeCorrect()
    {
        var obstacle = new Obstacle(latitude: 5, longitude: 10);

        Assert.Equal(5, obstacle.Position.Latitude);
        Assert.Equal(10, obstacle.Position.Longitude);
    }


}
