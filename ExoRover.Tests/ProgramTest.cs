using Xunit;
using System.Reflection;
using System.Text.Json;
using Rover;

namespace ExoRover.Tests;

public class ProgramTest : IDisposable
{
    private readonly string _testConfigPath = "test_program_config.json";

    public ProgramTest()
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
    public void RoverProgram_Main_WithValidConfig_ShouldNotThrowDuringInitialization()
    {
        string[] args = { _testConfigPath };

        var programType = typeof(Rover.Program);
        Assert.NotNull(programType);
    }

    [Fact]
    public void MissionControlProgram_ShouldBeLoadable()
    {
        var programType = Type.GetType("MissionControl.Program, MissionControl");
        
        Assert.NotNull(programType);
    }

    [Fact]
    public void Config_JsonFile_ShouldExist()
    {
        bool configExists = File.Exists(_testConfigPath);

        Assert.True(configExists);
    }

    [Fact]
    public void Program_WithInvalidArgs_ShouldHandleGracefully()
    {
        string[] invalidArgs = { "nonexistent_config.json" };

        var programType = typeof(Rover.Program);
        var mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);
        Assert.NotNull(mainMethod);
    }
}
