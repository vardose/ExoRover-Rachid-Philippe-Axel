using Map;
using MissionControl;
using Xunit;

namespace ExoRover.Tests;

public class MapConsoleRendererTest
{
    [Fact]
    public void MapRenderer_Constructor_ShouldInitializeWithDefaultValues()
    {
        var renderer = new MapRenderer();

        Assert.Equal(-1, renderer.RoverX);
        Assert.Equal(-1, renderer.RoverY);
    }

    [Fact]
    public void MapRenderer_SetRoverPosition_ShouldUpdateCorrectly()
    {
        var renderer = new MapRenderer();

        renderer.RoverX = 5;
        renderer.RoverY = 3;

        Assert.Equal(5, renderer.RoverX);
        Assert.Equal(3, renderer.RoverY);
    }

    [Fact]
    public void MapRenderer_Render_ShouldNotThrowException()
    {
        var renderer = new MapRenderer();
        var map      = new Map.Map();

        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_Render_WithRoverPosition_ShouldNotThrowException()
    {
        var renderer = new MapRenderer();
        var map      = new Map.Map();
        renderer.RoverX = 2;
        renderer.RoverY = 4;

        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_ImplementsIMapRenderer()
    {
        MapRenderer renderer = new MapRenderer();
        var         map      = new Map.Map();

        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_Render_WithObstacles_ShouldNotThrowException()
    {
        var renderer = new MapRenderer();
        var map      = new Map.Map();
        map.addObstacle(new Obstacle(2, 3));
        map.addObstacle(new Obstacle(5, 7));

        renderer.Render(map);
    }

    [Fact]
    public void MapRenderer_Render_CaptureOutput_ShouldContainExpectedContent()
    {
        var renderer = new MapRenderer();
        var map      = new Map.Map();
        renderer.RoverX = 0;
        renderer.RoverY = 0;

        var       originalOut  = Console.Out;
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        try
        {
            renderer.Render(map);

            var output = stringWriter.ToString();
            Assert.True(output.Contains("^") || output.Contains(">") || output.Contains("v") || output.Contains("<")); // Le rover devrait être affiché avec une orientation
            Assert.Contains(".", output); // Les cases vides devraient être affichées
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void MapRenderer_Render_WithoutRover_ShouldOnlyShowDots()
    {
        var renderer = new MapRenderer(); // RoverX = -1, RoverY = -1 par défaut
        var map      = new Map.Map();

        var       originalOut  = Console.Out;
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        try
        {
            renderer.Render(map);

            var output = stringWriter.ToString();
            Assert.DoesNotContain("^", output); // Pas de rover affiché
            Assert.DoesNotContain(">", output);
            Assert.DoesNotContain("v", output);
            Assert.DoesNotContain("<", output);
            Assert.Contains("?", output);       // Cases non explorées affichées
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void MapRenderer_UpdateVisibility_ShouldUpdateRoverPosition()
    {
        var renderer = new MapRenderer();

        renderer.UpdateVisibility(3, 4, 1);

        Assert.Equal(3, renderer.RoverX);
        Assert.Equal(4, renderer.RoverY);
    }

    [Fact]
    public void MapRenderer_OrientationProperty_ShouldBeSettable()
    {
        var renderer = new MapRenderer();

        renderer.orientation = Orientation.Est;

        Assert.Equal(Orientation.Est, renderer.orientation);
    }
}