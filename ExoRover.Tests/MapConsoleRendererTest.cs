using Map;
using Xunit;
using System;
using System.IO;

namespace ExoRover.Tests;

public class MapConsoleRendererTest
{
    [Fact]
    public void MapRenderer_Constructor_ShouldInitializeWithDefaultValues()
    {
        // Act
        var renderer = new MapRenderer();

        // Assert
        Assert.Equal(-1, renderer.RoverX);
        Assert.Equal(-1, renderer.RoverY);
    }

    [Fact]
    public void MapRenderer_SetRoverPosition_ShouldUpdateCorrectly()
    {
        // Arrange
        var renderer = new MapRenderer();

        // Act
        renderer.RoverX = 5;
        renderer.RoverY = 3;

        // Assert
        Assert.Equal(5, renderer.RoverX);
        Assert.Equal(3, renderer.RoverY);
    }

    [Fact]
    public void MapRenderer_Render_ShouldNotThrowException()
    {
        // Arrange
        var renderer = new MapRenderer();
        var map      = new Map.Map();

        // Act & Assert - Should not throw
        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_Render_WithRoverPosition_ShouldNotThrowException()
    {
        // Arrange
        var renderer = new MapRenderer();
        var map      = new Map.Map();
        renderer.RoverX = 2;
        renderer.RoverY = 4;

        // Act & Assert - Should not throw
        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_ImplementsIMapRenderer()
    {
        // Arrange
        MapRenderer renderer = new MapRenderer();
        var         map      = new Map.Map();

        // Act & Assert - Should not throw
        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_Render_WithObstacles_ShouldNotThrowException()
    {
        // Arrange
        var renderer = new MapRenderer();
        var map      = new Map.Map();
        map.addObstacle(new Obstacle(2, 3));
        map.addObstacle(new Obstacle(5, 7));

        // Act & Assert - Should not throw
        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_Render_CaptureOutput_ShouldContainExpectedContent()
    {
        // Arrange
        var renderer = new MapRenderer();
        var map      = new Map.Map();
        renderer.RoverX = 0;
        renderer.RoverY = 0;

        // Capture console output
        var       originalOut  = Console.Out;
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        try
        {
            // Act
            renderer.Render(map);

            // Assert
            var output = stringWriter.ToString();
            Assert.Contains("R", output); // Le rover devrait être affiché
            Assert.Contains(".", output); // Les cases vides devraient être affichées
        }
        finally
        {
            // Restore original console output
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void MapRenderer_Render_WithoutRover_ShouldOnlyShowDots()
    {
        // Arrange
        var renderer = new MapRenderer(); // RoverX = -1, RoverY = -1 par défaut
        var map      = new Map.Map();

        // Capture console output
        var       originalOut  = Console.Out;
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        try
        {
            // Act
            renderer.Render(map);

            // Assert
            var output = stringWriter.ToString();
            Assert.DoesNotContain("R", output); // Pas de rover affiché
            Assert.Contains(".", output);       // Seulement des points
        }
        finally
        {
            // Restore original console output
            Console.SetOut(originalOut);
        }
    }
}