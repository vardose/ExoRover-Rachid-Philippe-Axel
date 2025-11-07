using Rover;
using Xunit;

namespace ExoRover.Tests;

public class ConfigurationClassesTest
{
    [Fact]
    public void CommunicationConfig_ShouldInitializeProperties()
    {
        var commConfig = new CommunicationConfig
        {
            Host = "192.168.1.1",
            MissionControlPort = 9000,
            RoverPort = 9001
        };

        Assert.Equal("192.168.1.1", commConfig.Host);
        Assert.Equal(9000, commConfig.MissionControlPort);
        Assert.Equal(9001, commConfig.RoverPort);
    }

    [Fact]
    public void RoverSettingsConfig_ShouldInitializeProperties()
    {
        var roverSettings = new RoverSettingsConfig
        {
            Orientation = "Sud",
            InitialPosition = new List<int> { 3, 7 },
            isObstacleDetected = true
        };

        Assert.Equal("Sud", roverSettings.Orientation);
        Assert.Equal(new List<int> { 3, 7 }, roverSettings.InitialPosition);
        Assert.True(roverSettings.isObstacleDetected);
    }

    [Fact]
    public void CommunicationConfig_DefaultValues_ShouldBeNull()
    {
        var commConfig = new CommunicationConfig();

        Assert.Null(commConfig.Host);
        Assert.Equal(0, commConfig.MissionControlPort);  // int default
        Assert.Equal(0, commConfig.RoverPort);           // int default
    }

    [Fact]
    public void RoverSettingsConfig_DefaultValues_ShouldBeNull()
    {
        var roverSettings = new RoverSettingsConfig();

        Assert.Null(roverSettings.Orientation);
        Assert.Null(roverSettings.InitialPosition);
        Assert.False(roverSettings.isObstacleDetected); // bool default
    }

    [Fact]
    public void Config_Properties_ShouldBeSettable()
    {
        var config = new Config();
        var commConfig = new CommunicationConfig { Host = "localhost" };
        var roverSettings = new RoverSettingsConfig { Orientation = "Nord" };

        config.Communication = commConfig;
        config.RoverSettings = roverSettings;

        Assert.Same(commConfig, config.Communication);
        Assert.Same(roverSettings, config.RoverSettings);
        Assert.Equal("localhost", config.Communication.Host);
        Assert.Equal("Nord", config.RoverSettings.Orientation);
    }
}
