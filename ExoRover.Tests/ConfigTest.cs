using Rover;
using Xunit;
using System.Text.Json;

namespace ExoRover.Tests;

public class ConfigTest : IDisposable
{
    private readonly string _testConfigPath = "test_config.json";

    public void Dispose()
    {
        if (File.Exists(_testConfigPath))
        {
            File.Delete(_testConfigPath);
        }
    }

    [Fact]
    public void Config_Load_ValidFile_ShouldReturnCorrectConfig()
    {
        var testConfig = new
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

        var json = JsonSerializer.Serialize(testConfig, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_testConfigPath, json);

        var config = Config.Load(_testConfigPath);

        Assert.NotNull(config);
        Assert.NotNull(config.Communication);
        Assert.NotNull(config.RoverSettings);
        Assert.Equal("127.0.0.1", config.Communication.Host);
        Assert.Equal(8080, config.Communication.MissionControlPort);
        Assert.Equal(8081, config.Communication.RoverPort);
        Assert.Equal("Nord", config.RoverSettings.Orientation);
        Assert.Equal(new[] { 0, 0 }, config.RoverSettings.InitialPosition);
        Assert.False(config.RoverSettings.isObstacleDetected);

        Dispose();
    }

    [Fact]
    public void Config_Load_NonExistentFile_ShouldThrowFileNotFoundException()
    {
        string nonExistentPath = "non_existent_file.json";

        Assert.Throws<FileNotFoundException>(() => Config.Load(nonExistentPath));
    }

    [Fact]
    public void Config_Load_InvalidJson_ShouldThrowException()
    {
        File.WriteAllText(_testConfigPath, "invalid json content");

        Assert.ThrowsAny<Exception>(() => Config.Load(_testConfigPath));

        Dispose();
    }

    [Fact]
    public void Config_Load_EmptyFile_ShouldThrowException()
    {
        File.WriteAllText(_testConfigPath, "");

        Assert.ThrowsAny<Exception>(() => Config.Load(_testConfigPath));

        Dispose();
    }

    [Fact]
    public void Config_Load_NullJson_ShouldThrowException()
    {
        File.WriteAllText(_testConfigPath, "null");

        Assert.ThrowsAny<Exception>(() => Config.Load(_testConfigPath));

        Dispose();
    }
}