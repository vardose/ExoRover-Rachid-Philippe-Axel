using Rover;
using Xunit;
using System;
using System.IO;
using System.Text.Json;

namespace ExoRover.Tests;

public class ConfigTest
{
    private readonly string _testConfigPath = "test_config.json";

    public void Dispose()
    {
        // Nettoyer le fichier de test après chaque test
        if (File.Exists(_testConfigPath))
        {
            File.Delete(_testConfigPath);
        }
    }

    [Fact]
    public void Config_Load_ValidFile_ShouldReturnCorrectConfig()
    {
        // Arrange
        var testConfig = new
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

        var json = JsonSerializer.Serialize(testConfig, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_testConfigPath, json);

        // Act
        var config = Config.Load(_testConfigPath);

        // Assert
        Assert.NotNull(config);
        Assert.NotNull(config.Communication);
        Assert.NotNull(config.RoverSettings);
        Assert.Equal("127.0.0.1", config.Communication.Host);
        Assert.Equal(8080, config.Communication.MissionControlPort);
        Assert.Equal(8081, config.Communication.RoverPort);
        Assert.Equal("Nord", config.RoverSettings.Orientation);
        Assert.Equal(new[] { 0, 0 }, config.RoverSettings.InitialPosition);
        Assert.False(config.RoverSettings.isObstacleDetected);

        // Cleanup
        Dispose();
    }

    [Fact]
    public void Config_Load_NonExistentFile_ShouldThrowFileNotFoundException()
    {
        // Arrange
        string nonExistentPath = "non_existent_file.json";

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => Config.Load(nonExistentPath));
    }

    [Fact]
    public void Config_Load_InvalidJson_ShouldThrowException()
    {
        // Arrange
        File.WriteAllText(_testConfigPath, "invalid json content");

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => Config.Load(_testConfigPath));

        // Cleanup
        Dispose();
    }

    [Fact]
    public void Config_Load_EmptyFile_ShouldThrowException()
    {
        // Arrange
        File.WriteAllText(_testConfigPath, "");

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => Config.Load(_testConfigPath));

        // Cleanup
        Dispose();
    }

    [Fact]
    public void Config_Load_NullJson_ShouldThrowException()
    {
        // Arrange
        File.WriteAllText(_testConfigPath, "null");

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => Config.Load(_testConfigPath));

        // Cleanup
        Dispose();
    }
}
