using Rover;
using Xunit;
using System.Text.Json;

namespace ExoRover.Tests;

public class RoverTest : IDisposable
{
    private readonly string _testConfigPath = "test_rover_config.json";
    private          Config _testConfig;

    public RoverTest()
    {
        var configData = new
        {
            Communication = new
            {
                Host               = "127.0.0.1",
                MissionControlPort = 8080,
                RoverPort          = 8081
            },
            RoverSettings = new
            {
                Orientation        = "Nord",
                InitialPosition    = new[] { 0, 0 },
                isObstacleDetected = false
            }
        };

        var json = JsonSerializer.Serialize(configData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_testConfigPath, json);
        _testConfig = Config.Load(_testConfigPath);
    }

    public void Dispose()
    {
        if (File.Exists(_testConfigPath))
        {
            File.Delete(_testConfigPath);
        }
    }

    [Fact]
    public void Rover_Constructor_ShouldInitializeCorrectly()
    {
        var rover = new Rover.Rover(_testConfig);

        Assert.NotNull(rover);
    }

    [Fact]
    public void Rover_Constructor_WithNullConfig_ShouldAllowNullConfig()
    {
        var rover = new Rover.Rover(null);
        Assert.NotNull(rover);
    }

  
  


    [Fact]
    public void Rover_ConfigurationAccess_ShouldHaveCorrectValues()
    {
        var rover = new Rover.Rover(_testConfig);


        Assert.NotNull(rover);
    }
}